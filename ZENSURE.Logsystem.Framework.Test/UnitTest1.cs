using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZENSURE.Logsystem.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "http://192.168.2.115:23649/api/values";
           
            Assert.AreEqual(200, HttpSingleton.Instance.Get(url));
        }
    }
}
