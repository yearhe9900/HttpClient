using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        /// <summary>
        /// GET Request
        /// </summary>
        /// <param name="url">Request Url String</param>
        /// <returns>(result:Is legal result,errorMsg:If there are errors,the error msg is returned)</returns>
        public (string result, HttpStatusCode code, string errorMsg) Get(string url)
        {
            var (result, errorMsg) = CheckParameters(url);
            if (result)
            {
                using (HttpResponseMessage response = _httpClient.GetAsync(new Uri(url)).Result)
                {
                    return GetResponseResult(response);
                }
            }
            return (string.Empty, HttpStatusCode.BadRequest, errorMsg);
        }

        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="url">Request Url String</param>
        /// <param name="json">Request Json Data</param>
        /// <returns>(result:Is legal result,errorMsg:If there are errors,the error msg is returned)</returns>
        public (string result, HttpStatusCode code, string errorMsg) Post(string url, string json, string header = null)
        {
            var (result, errorMsg) = CheckParameters(url);
            if (result)
            {
                using (HttpContent httpContent = new StringContent(json))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/josn");

                    using (HttpResponseMessage response = _httpClient.PostAsync(url, httpContent).Result)
                    {
                        return GetResponseResult(response);
                    }
                }
            }
            return (string.Empty, HttpStatusCode.BadRequest, errorMsg);
        }

        /// <summary>
        /// Determine whether string is legal
        /// </summary>
        /// <param name="url">Url String</param>
        /// <returns>(result:Is legal result,errorMsg:If there are errors,the error msg is returned)</returns>
        private (bool result, string errorMsg) CheckParameters(string url)
        {
            string urlMatch = @"[a-zA-z]+://[^\s]*";

            if (Regex.IsMatch(url, urlMatch))
            {
                return (true, "OK");
            }
            else
            {
                return (false, "Url is not legal!");
            }
        }

        /// <summary>
        /// Get Finally Response
        /// </summary>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns></returns>
        private (string result, HttpStatusCode code, string errorMsg) GetResponseResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                return (t.Result, response.StatusCode, response.StatusCode.ToString());
            }
            else
            {
                return (string.Empty, response.StatusCode, response.StatusCode.ToString());
            }
        }
    }
}
