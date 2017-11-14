using Learning.Zookeeper.Exceptions;
using Learning.Zookeeper.Models;
using Learning.Zookeeper.Util.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Zookeeper.Internals
{
    public class RemoteConfigRepository
    {
        private ApolloSettings _apolloSettings;
        private string _namespaceName;
        private CancellationTokenSource _cancellationTokenSource;
        private ManualResetEventSlim _eventSlim;
        private ApolloConfig _apolloConfig;
        private string localIp="127.0.0.1";
        public RemoteConfigRepository(IOptions<ApolloConfig> apolloConfig, IOptions<ApolloSettings> apolloSettings,string namespaceName= "application")
        {
            InitScheduleRefresh();
            _namespaceName = namespaceName;
            _apolloConfig = apolloConfig.Value;
            _apolloSettings = apolloSettings.Value;
        }

        protected bool TrySync()
        {
            try
            {
                Sync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log
            }
            return false;
        }

        protected async Task Sync()
        {
            try
            {
                await LoadApolloConfig();
                //获取远程ApolloSetting，若有变更则更新
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApolloConfig> LoadApolloConfig()
        {
            int maxRetries = 2;
            Exception exception = null;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var url = BuildUrlFromSetting(_apolloSettings);
                    var response =await HttpUtil.DoGet<ApolloConfig>(new HttpRequest(url));
                    return response.Body;
                }
                catch (ApolloConfigStatusCodeException ex)
                {
                    //config not found
                    if (ex.StatusCode == 404)
                    {
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                Thread.Sleep(1000); //sleep 1 second
            }
            throw new ApolloConfigException("error",exception);
        }

        private string BuildUrlFromSetting(ApolloSettings setting)
        {
            string uri = setting.Url;
            if (!uri.EndsWith("/", StringComparison.Ordinal))
            {
                uri += "/";
            }
            //Looks like .Net will handle all the url encoding for me...
            uri = $"{uri}configs/{setting.AppID}/{setting.Cluster}/{_namespaceName}";
            var query = string.Empty; ;
            if (!string.IsNullOrEmpty(localIp))
            {
                query =$"{query}&ip={localIp}";
            }
            return $"{uri}?{query}";
        }

        /// <summary>
        /// 定时刷新
        /// </summary>
        private  void InitScheduleRefresh()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _eventSlim = new ManualResetEventSlim(false, spinCount: 1);
            var _processQueueTask = Task.Factory.StartNew(ScheduleRefresh, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void ScheduleRefresh()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                //Thread.Sleep(TimeSpan.FromSeconds(5));
                Task.Factory.StartNew(() => { Thread.Sleep(TimeSpan.FromSeconds(5)); _eventSlim.Set(); });
                _apolloConfig = LoadApolloConfig().GetAwaiter().GetResult();
                Console.WriteLine(_apolloConfig.ToString());
                try
                {
                    _eventSlim.Wait(_cancellationTokenSource.Token);
                    _eventSlim.Reset();
                }
                catch (OperationCanceledException)
                {
                    // expected
                    break;
                }
            }
        }

        /// <summary>
        /// 长连接
        /// </summary>
        private async Task ScheduleLongPollingRefresh()
        {
        }
    }
}
