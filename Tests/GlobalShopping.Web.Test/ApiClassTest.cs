using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalShopping.Web.Middleware;

namespace GlobalShopping.Web.Test
{
    [TestClass]
    public class ApiClassTest
    {
        [TestMethod]
        public void GetApiClass()
        {
            var path = "/_api/test/test1";

            var api = new ApiMiddleware(null);
            var apiClass = api.GetApiClass(path);

            Assert.AreEqual(apiClass.Class, "test");
            Assert.AreEqual(apiClass.Method, "test1");
        }
    }
}
