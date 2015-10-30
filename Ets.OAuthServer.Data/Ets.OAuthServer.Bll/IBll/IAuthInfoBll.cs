using System.Threading.Tasks;

namespace Ets.OAuthServer.Bll.IBll
{
    public interface IAuthInfoBll
    {
        Task<string> GetReturnUrl(string clientId);

        Task<bool> IsClientExist(string clientId, string secret);
    }
}