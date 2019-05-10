using System;
using System.Net.Http;
using ZENSURE.Logsystem.Model;

namespace ZENSURE.Logsystem
{
    public class HttpSingleton
    {
        private static HttpClient _httpClient = null;

        private HttpSingleton()
        {
            HttpClient httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            _httpClient = httpClient;
        }

        private static readonly Lazy<HttpSingleton> lazy = new Lazy<HttpSingleton>(() => { return new HttpSingleton(); });

        public static HttpSingleton Instance => lazy.Value;

        public string Get(string url)
        {
            HttpResponseMessage response = _httpClient.GetAsync(new Uri(url)).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
