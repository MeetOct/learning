using Learning.Zookeeper.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Learning.Zookeeper.Util.Http
{
    public static class HttpUtil
    {
        private static string basicAuth= "Basic" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("user:"));
        private static HttpClient _httpClient = new HttpClient();

        public static async Task<HttpResponse<T>> DoGet<T>(HttpRequest httpRequest)
        {
            int statusCode;
            try
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri=new Uri(httpRequest.Url),
                    Method = HttpMethod.Get,
                };
                //request.Headers.Authorization = new AuthenticationHeaderValue(basicAuth);
                int timeout = httpRequest.Timeout;
                //if (timeout <= 0 && timeout != Timeout.Infinite)
                //{
                //    timeout = m_configUtil.Timeout;
                //}
                //int readTimeout = httpRequest.ReadTimeout;
                //if (readTimeout <= 0 && readTimeout != Timeout.Infinite)
                //{
                //    readTimeout = m_configUtil.ReadTimeout;
                //}
                if (timeout > 0)
                {
                _httpClient.Timeout = TimeSpan.FromMinutes(timeout);
                }
                var resp =await _httpClient.SendAsync(request);
                statusCode = (int)resp.StatusCode;
                if (statusCode == 200)
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    T body = JsonConvert.DeserializeObject<T>(content);
                    return new HttpResponse<T>(statusCode, body);
                }
                if (statusCode == 304)
                {
                    return new HttpResponse<T>(statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new ApolloConfigException("Could not complete get operation", ex);
            }

            throw new ApolloConfigStatusCodeException(statusCode, string.Format("Get operation failed for {0}", httpRequest.Url));
        }
    }
}
