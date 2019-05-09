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
            Class1 class1 = new Class1();
            Assert.AreEqual(200, class1.A().Code);
        }
    }
}
