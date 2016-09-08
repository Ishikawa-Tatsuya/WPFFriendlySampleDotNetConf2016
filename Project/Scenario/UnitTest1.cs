using Microsoft.VisualStudio.TestTools.UnitTesting;
using Driver;

namespace Scenario
{
    [TestClass]
    public class UnitTest1
    {
        AppDriver _app;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new AppDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _app.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
