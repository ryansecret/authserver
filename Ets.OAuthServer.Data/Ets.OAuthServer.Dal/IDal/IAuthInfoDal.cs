using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal.IDal
{
    public interface IAuthInfoDal
    {
        Task<string> GetReturnUrl(string clientId);

        Task<bool> IsClientExist(string clientId, string secret);
    }
}
