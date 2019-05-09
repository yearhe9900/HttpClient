using System;
using System.Net.Http;
using ZENSURE.Logsystem.Model;

namespace ZENSURE.Logsystem
{
    public class Class1
    {
        public ResponseResultBaseModel A()
        {
            HttpClient httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            string url = "http://192.168.2.115:23649/api/values";
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;
            var result = response.Content.ReadAsStringAsync().Result.ToData<ResponseResultBaseModel>();

            HttpResponseMessage response2 = httpClient.GetAsync(new Uri(url)).Result;
            var result2 = response2.Content.ReadAsStringAsync().Result.ToData<ResponseResultBaseModel>();

            return result;
        }
    }
}
