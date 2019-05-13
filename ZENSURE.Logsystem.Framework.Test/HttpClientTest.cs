using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZENSURE.Logsystem.Model;
using ZENSURE.Logsystem.TestModel;
using ZENSURE.LogSystem.Enums;

namespace ZENSURE.Logsystem.Framework.Test
{
    [TestClass]
    public class HttpClientTest
    {
        /// <summary>
        /// 单次GET测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_BY_LEGAL_URL()
        {
            Assert.IsTrue(StringExpand.IsUrl(TestStaticString._getLegalUrl));

            Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Get(TestStaticString._getLegalUrl).code);
        }

        /// <summary>
        /// 多次GET测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_MANY_TIMES_BY_LEGAL_URL()
        {
            Assert.IsTrue(StringExpand.IsUrl(TestStaticString._getLegalUrl));

            Dictionary<string, string> headers = new Dictionary<string, string>() { };
            headers.Add("Content-Type", "application/xml");
            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            for (int i = 0; i < 10000; i++)
            {
                Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Get(TestStaticString._getLegalUrl, headers).code);
            }
        }

        /// <summary>
        /// 单次GET测试_
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_BY_UOTFOUND_URL()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, HttpSingleton.Instance.Get(TestStaticString._getNotFoundUrl).code);
        }

        /// <summary>
        /// 单次POST测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_POST_BY_LEGAL_URL()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            var (timestamp, sign) = StringExpand.GetTimestampAndSign();

            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Post(TestStaticString._postLegalUrl, TestStaticString._jsonData, headers).code);
        }

        /// <summary>
        /// 多次POST测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_POST_MANY_TIMES_BY_LEGAL_URL()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            var (timestamp, sign) = StringExpand.GetTimestampAndSign();

            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            for (var i = 0; i < 10000; i++)
            {
                Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Post(TestStaticString._postLegalUrl, TestStaticString._jsonData, headers).code);
            }
        }

        /// <summary>
        /// 单次发送系统日志测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_POST_SEND_SYS_LOG_BY_LEGAL_URL()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            var (timestamp, sign) = StringExpand.GetTimestampAndSign();

            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            var model = new SystemLogModel
            {
                Source = "zlead",
                Host = "192.168.1.2",
                App = AppEnum.ANDROID,
                Type = RequestTypeEnum.API,
                Url = "https://www.baidu.com",
                Mode = ModeEnum.GET,
                Level = LevelEnum.DEBUG,
                Date = DateTime.Now,
                Edition = "v1.1.1",
                Message = "这是测试数据02"
            };

            Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.PostSendSystemLog(TestStaticString._postLegalUrl, model, headers).code);
        }
    }
}
