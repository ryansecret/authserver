using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ets.OAuthServer.Dal.IDal;

namespace Ets.OAuthServer.Dal.Dal
{
    public class AuthInfoDal:IAuthInfoDal
    {
        
        public async Task<string> GetReturnUrl(string clientId)
        {
            var sql = "SELECT  CallbackUrl FROM Application where AppKey=@AppKey";
            using (var con=Database.GetConnection())
            {
               return  await con.ExecuteScalarAsync<string>(sql, new { AppKey =clientId});
            }
        }

        public async Task<bool> IsClientExist(string clientId, string secret)
        {
            var sql = "SELECT  count(1) FROM Application where AppKey=@AppKey and AppSecret=@AppSecret";
            using (var con=Database.GetConnection())
            {
              var result=  await con.ExecuteScalarAsync<int>(sql, new { AppKey = clientId, AppSecret =secret}) ;
                return result > 0;
            }
        }
    }
}
