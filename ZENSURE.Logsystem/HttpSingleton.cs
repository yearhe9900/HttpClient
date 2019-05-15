using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ZENSURE.Logsystem.Model;
using Newtonsoft.Json;
using ZENSURE.Logsystem.Const;
using ZENSURE.Logsystem.Utils;

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
        /// <param name="headers">Request Headers</param>
        /// <returns>(result:Is legal result,code:HttpStatusCode,errorMsg:If there are errors,the error msg is returned)</returns>
        public (string result, HttpStatusCode code, string errorMsg) Get(string url, Dictionary<string, string> headers = null)
        {
            var (result, errorMsg) = CheckParameters(url);
            if (result)
            {
                DefaultRequestHeadersAdd(headers);
                using (HttpResponseMessage response = _httpClient.GetAsync(new Uri(url)).Result)
                {
                    DefaultRequestHeadersClear(headers);
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
        /// <param name="headers">Request Headers</param>
        /// <param name="contentType">Request Content Type</param>
        /// <param name="timeOut">Request Time Out</param>
        /// <returns>(result:Is legal result,code:HttpStatusCode,errorMsg:If there are errors,the error msg is returned)</returns>
        public (string result, HttpStatusCode code, string errorMsg) Post(string url, string postData = null, Dictionary<string, string> headers = null, string contentType = null, int timeOut = 0)
        {
            var (result, errorMsg) = CheckParameters(url);
            if (result)
            {
                if (timeOut > 0)
                {
                    _httpClient.Timeout = new TimeSpan(0, 0, timeOut);
                }
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    DefaultRequestHeadersAdd(headers);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrWhiteSpace(contentType) ? ContentTypeConst.JSON : contentType);
                    using (HttpResponseMessage response = _httpClient.PostAsync(url, httpContent).Result)
                    {
                        DefaultRequestHeadersClear(headers);
                        return GetResponseResult(response);
                    }
                }
            }
            return (string.Empty, HttpStatusCode.BadRequest, errorMsg);
        }

        /// <summary>
        /// Post Send System Log
        /// </summary>
        /// <param name="url">Request Url String</param>
        /// <param name="model">Request Post Data</param>
        /// <returns>(result:Is legal result,code:HttpStatusCode,errorMsg:If there are errors,the error msg is returned)</returns>
        /// <returns></returns>
        public (string result, HttpStatusCode code, string errorMsg) PostSendLog<T>(string url, T model) where T : BaseLogModel
        {
            if (string.IsNullOrWhiteSpace(model.Source))
            {
                return (string.Empty, HttpStatusCode.BadRequest, @"The input parameter ""appkey"" is not allowed to be null");
            }

            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            var (timestamp, sign) = GetTimestampAndSign(model.Source);

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            return Post(url, JsonConvert.SerializeObject(model), headers);
        }

        ///// <summary>
        ///// Post Send Interface Log
        ///// </summary>
        ///// <param name="url">Request Url String</param>
        ///// <param name="model">Request Post Data</param>
        ///// <param name="headers">Request Headers</param>
        ///// <param name="contentType">Request Content Type</param>
        ///// <param name="timeOut">Request Time Out</param>
        ///// <returns>(result:Is legal result,code:HttpStatusCode,errorMsg:If there are errors,the error msg is returned)</returns>
        ///// <returns></returns>
        //public (string result, HttpStatusCode code, string errorMsg) PostSendInterfaceLog(string url, InterfaceLogModel model, Dictionary<string, string> headers = null, string contentType = null, int timeOut = 0)
        //{
        //    return Post(url, JsonConvert.SerializeObject(model ?? new InterfaceLogModel()), headers, contentType, timeOut);
        //}

        #region Privite Function

        /// <summary>
        /// Add Default Headers
        /// </summary>
        /// <param name="headers"></param>
        private void DefaultRequestHeadersAdd(Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                //清空所有header内容
                _httpClient.DefaultRequestHeaders.Clear();

                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        ///// Clear Default Headers
        /// </summary>
        /// <param name="headers"></param>
        private void DefaultRequestHeadersClear(Dictionary<string, string> headers = null)
        {
            //清空所有header内容
            _httpClient.DefaultRequestHeaders.Clear();

            //重新赋值user-agent
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
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

        /// <summary>
        /// Get Timestamp And Sign
        /// </summary>
        /// <returns></returns>
        private (string timestamp, string sign) GetTimestampAndSign(string appkey)
        {
            var epoch = TimestampHelper.GetUnixTimeStampByDatetime(DateTime.Now.ToUniversalTime());

            var sign = MD5Helper.StrToMD5With32(epoch + appkey);

            return (epoch.ToString(), sign);
        }
        #endregion
    }
}
