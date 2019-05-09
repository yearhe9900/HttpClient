using System;
using Xunit;

namespace ZENSURE.Logsystem.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1 = new Class1();
            Assert.Equal(200, class1.A().Code);
        }
    }
}
