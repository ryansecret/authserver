using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using Ets.OAuthServer.Dal.Dal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ets.OAuthServer.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public class DapperUserStore : UserStore<ApplicationUser>
    {
        public DapperUserStore(DbContext dbContext):base(dbContext){}


        public override async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            string sql = "Select * from AspNetUsers where PhoneNumber=@UserName";
            ApplicationUser user = new ApplicationUser();
            using (var conn = DbManager.GetConnection())
            {
                user = conn.Query<ApplicationUser>(sql, new { UserName = userName }).FirstOrDefault();
            }
            return await Task.Run(() => user);
        }

        
    }
}