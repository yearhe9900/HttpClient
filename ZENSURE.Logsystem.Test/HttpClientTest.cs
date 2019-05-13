using System;
using System.Collections.Generic;
using System.Net;
using Xunit;
using ZENSURE.Logsystem.TestModel;

namespace ZENSURE.Logsystem.Test
{
    public class HttpClientTest
    {
        /// <summary>
        /// ����GET����
        /// </summary>
        [Fact]
        public void TEST_HTTP_GET_BY_LEGAL_URL()
        {
            Assert.True(StringExpand.IsUrl(TestStaticString._getLegalUrl));

            Dictionary<string, string> headers = new Dictionary<string, string>() { };
            headers.Add("Content-Type", "application/xml");
            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            Assert.Equal(HttpStatusCode.OK, HttpSingleton.Instance.Get(TestStaticString._getLegalUrl, headers).code);
        }

        /// <summary>
        /// ���GET����
        /// </summary>
        [Fact]
        public void TEST_HTTP_GET_MANY_TIMES_BY_LEGAL_URL()
        {
            Assert.True(StringExpand.IsUrl(TestStaticString._getLegalUrl));

            Dictionary<string, string> headers = new Dictionary<string, string>() { };
            headers.Add("Content-Type", "application/xml");
            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            for (int i = 0; i < 10000; i++)
            {
                Assert.Equal(HttpStatusCode.OK, HttpSingleton.Instance.Get(TestStaticString._getLegalUrl, headers).code);
            }
        }

        /// <summary>
        /// ����GET����_�����ַ
        /// </summary>
        [Fact]
        public void TEST_HTTP_GET_BY_UOTFOUND_URL()
        {
            Assert.Equal(HttpStatusCode.NotFound, HttpSingleton.Instance.Get(TestStaticString._getNotFoundUrl).code);
        }


        /// <summary>
        /// ����POST����
        /// </summary>
        [Fact]
        public void TEST_HTTP_POST_BY_LEGAL_URL()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            var (timestamp, sign) = StringExpand.GetTimestampAndSign();

            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            Assert.Equal(HttpStatusCode.OK, HttpSingleton.Instance.Post(TestStaticString._postLegalUrl, TestStaticString._jsonData, headers).code);
        }

        /// <summary>
        /// ���POST����
        /// </summary>
        [Fact]
        public void TEST_HTTP_POST_MANY_TIMES_BY_LEGAL_URL()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { };

            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            var (timestamp, sign) = StringExpand.GetTimestampAndSign();

            headers.Add("Sign", sign);
            headers.Add("Timestamp", timestamp);

            for (var i = 0; i < 10000; i++)
            {
                Assert.Equal(HttpStatusCode.OK, HttpSingleton.Instance.Post(TestStaticString._postLegalUrl, TestStaticString._jsonData, headers).code);
            }
        }

    }
}
