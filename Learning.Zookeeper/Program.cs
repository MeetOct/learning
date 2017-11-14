using Learning.Zookeeper.Internals;
using Learning.Zookeeper.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using org.apache.zookeeper;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static org.apache.zookeeper.KeeperException;

namespace Learning.Zookeeper
{
    #region Zookeeper
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        MainAsync(args).GetAwaiter().GetResult();
    //    }
    //    static async Task MainAsync(string[] args)
    //    {
    //        for (int i = 0; i < 5; i++)
    //        {
    //            Thread thread = new Thread(new ThreadStart(Invoke));
    //            thread.Start();
    //        }
    //        Console.ReadKey();
    //    }

    //    static void Invoke()
    //    {
    //        try
    //        {
    //            var client = new ZookeeperClient(new ZookeeperClientOptions("120.77.65.127:2181")
    //            {
    //                BasePath = "/", //default value
    //                ConnectionTimeout = TimeSpan.FromSeconds(10), //default value
    //                SessionTimeout = TimeSpan.FromSeconds(20), //default value
    //                OperatingTimeout = TimeSpan.FromSeconds(60), //default value
    //                ReadOnly = false, //default value
    //                SessionId = 0, //default value
    //                SessionPasswd = null,//default value
    //                EnableEphemeralNodeRestore = true //default value
    //            });
    //            var result = client.CreatePersistentAsync("/test2", Encoding.ASCII.GetBytes("job start"));
    //            result.Wait();

    //            client.DeleteAsync("/test2").Wait();
    //            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}---succeed");
    //        }
    //        catch (KeeperException e)
    //        {
    //            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}---failed,{e.Message}");
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}---failed，{e.Message}");
    //        }
    //    }
    //} 
    #endregion

    public class Controller
    {
        public readonly RemoteConfigRepository _configRepository;
        private ApolloConfig _apolloConfig;

        public Controller(IOptions<ApolloConfig> apolloConfig, RemoteConfigRepository configRepository)
        {
            _configRepository = configRepository;
            _apolloConfig = apolloConfig.Value;
        }

        public async Task ShowApolloConfig(HttpContext context)
        {
            var config = await _configRepository.LoadApolloConfig();
            _apolloConfig = config;
            await context.Response.WriteAsync(_apolloConfig.ToString());
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // reloadOnChange: true is required for config changes to be detected.
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IApplicationBuilder app)
        {
            //1. 轮询读取配置
            //2.长连接推送通知实现实时更新
            app.Run(DisplayTimeAsync);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Simple mockup of a simple per request controller.
            services.AddScoped<Controller>();
            services.AddSingleton<RemoteConfigRepository>();
            // Binds config.json to the options and setups the change tracking.
            services.Configure<ApolloSettings>(Configuration.GetSection("Apollo"));
            services.Configure<ApolloConfig>(ac=> { });
        }

        public Task DisplayTimeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            return context.RequestServices.GetRequiredService<Controller>().ShowApolloConfig(context);
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}