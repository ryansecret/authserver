using System.Threading.Tasks;
using Ets.OAuthServer.Bll.IBll;
using Ets.OAuthServer.Dal.IDal;

namespace Ets.OAuthServer.Bll.Bll
{
    public class AuthInfoBll : IAuthInfoBll
    {
        private readonly IAuthInfoDal _dal;
        public AuthInfoBll(IAuthInfoDal testDal)
        {
            _dal = testDal;
        }
        

        public Task<string> GetReturnUrl(string clientId)
        {
            return _dal.GetReturnUrl(clientId);
        }

        public Task<bool> IsClientExist(string clientId, string secret)
        {
            return _dal.IsClientExist(clientId, secret);
        }
    }
}
