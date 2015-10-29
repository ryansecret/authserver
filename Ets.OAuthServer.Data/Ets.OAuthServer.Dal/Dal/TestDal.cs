using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ets.OAuthServer.Dal.IDal;

namespace Ets.OAuthServer.Dal.Dal
{
    public class TestDal : ITestDal
    {
        public string Test()
        {
            return "ryan";
        }
    }
}
