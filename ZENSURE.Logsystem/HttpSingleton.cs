using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem
{
    public class HttpSingleton
    {
        private static HttpClient _httpClient = null;
        private static List<string> _contentTypeList = null;

        private HttpSingleton()
        {
            HttpClient httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
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
        /// <param name="postData">Request Post Data</param>
        /// <returns>(result:Is legal result,errorMsg:If there are errors,the error msg is returned)</returns>
        public (string result, HttpStatusCode code, string errorMsg) Post(string url, string postData = null, Dictionary<string, string> headers = null, string contentType = null, int timeOut = 30)
        {
            var (result, errorMsg) = CheckParameters(url);
            if (result)
            {
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    else
                    {
                        _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                    }
                    using (HttpResponseMessage response = _httpClient.PostAsync(url, httpContent).Result)
                    {
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(string.IsNullOrWhiteSpace(contentType) ? ContentTypeConst.JSON : contentType);
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
