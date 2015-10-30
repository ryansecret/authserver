using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ets.OAuthServer.Model;
using Ets.OAuthServer.Dal.Dal;
using System.Linq;
namespace Ets.OAuthServer.Dal.Test
{
    [TestClass]
    public class ApplicationDalTest
    {
        ApplicationDal dal = new ApplicationDal();
        

        [TestMethod]
        [Description("添加测试")]
        public void AddTest()
        {
            var app = Create();

            dal.Add(app);

            var actual = dal.FindById(app.Id);

            AssertApplication(app, actual);
        }
        [TestMethod]
        [Description("根据APPKEY查询测试")]
        public void FindByAppKeyTest()
        {
            var app = Create();
            dal.Add(app);

            var actual = dal.FindByKey(app.AppKey);
            this.AssertApplication(app, actual);
        }
        [TestMethod]
        [Description("更新测试")]
        public void UpdateTest()
        {
            var app = Create();
            dal.Add(app);

            app.AppKey = "adfdsafdddddd";
            app.AppSecret = "sdfd3222";
            app.CallbackUrl = "http:/sfdasfdasf.co";

            dal.Update(app);

            var actual = dal.FindById(app.Id);

            AssertApplication(app, actual);
        }

        [TestMethod]
        [Description("列表测试")]
        public void ListTest()
        {
            var app1 = Create();
            dal.Add(app1);
            var app2 = Create();
            dal.Add(app2);

            var list = dal.List();
            Assert.IsTrue(list.Count() > 0);
        }

        [TestCleanup]
        public void Clear()
        {
            dal.DeleteAll();
        }

        public void AssertApplication(Application expected, Application actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.AppKey, actual.AppKey);
            Assert.AreEqual(expected.AppSecret, actual.AppSecret);
            Assert.AreEqual(expected.CallbackUrl, actual.CallbackUrl);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        public static Application Create()
        {
            var app = new Application()
            {
                AppKey = "sdfdsf",
                AppSecret = "dfadfd",
                CallbackUrl = "http://sdakfldjfldksjf"
            };
            return app;
        }
    }
}
