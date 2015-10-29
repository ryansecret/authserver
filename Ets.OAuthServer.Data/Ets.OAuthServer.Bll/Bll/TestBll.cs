using Ets.OAuthServer.Bll.IBll;
using Ets.OAuthServer.Dal.IDal;

namespace Ets.OAuthServer.Bll.Bll
{
    public class TestBll : ITestBll
    {
        private readonly ITestDal _testDal;
        public TestBll(ITestDal testDal)
        {
            _testDal = testDal;
        }
        public string Test()
        {
           return _testDal.Test();
        }
    }
}
