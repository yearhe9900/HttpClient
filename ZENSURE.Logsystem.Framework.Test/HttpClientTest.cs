using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZENSURE.Logsystem.Framework.Test
{
    [TestClass]
    public class HttpClientTest
    {
        private readonly static string _getLegalUrl = "http://192.168.2.115:23649/api/values";

        private readonly static string _getNotFoundUrl = "http://192.168.2.115:23649/api/value";

        /// <summary>
        /// 单次GET测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_BY_LEGAL_URL()
        {
            Assert.IsTrue(StringExpand.IsUrl(_getLegalUrl));

            Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Get(_getLegalUrl).code);
        }

        /// <summary>
        /// 多次GET测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_MANY_TIMES_BY_LEGAL_URL()
        {
            Assert.IsTrue(StringExpand.IsUrl(_getLegalUrl));

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(HttpStatusCode.OK, HttpSingleton.Instance.Get(_getLegalUrl).code);
            }
        }

        /// <summary>
        /// 单次GET测试_
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_BY_UOTFOUND_URL()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, HttpSingleton.Instance.Get(_getNotFoundUrl).code);
        }

        /// <summary>
        /// 多次GET测试
        /// </summary>
        [TestMethod]
        public void TEST_HTTP_GET_MANY_UNTIMES_BY_UOTFOUND_URL()
        {
            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, HttpSingleton.Instance.Get(_getNotFoundUrl).code);
            }
        }
    }
}
