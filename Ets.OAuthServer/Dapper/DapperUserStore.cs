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
 

namespace Ets.OAuthServer.Dapper
{
    /// <summary>
    /// Dapper链接数据库
    /// </summary>
    /// 创建者:杨力
    /// 创建日期:10/29/2015 17:13 PM
    /// 修改者:
    /// 修改时间:
    /// </summary>
    public class DapperUserStore : UserStore<ApplicationUser>
    {
        public DapperUserStore(DbContext dbContext) : base(dbContext)
        {
            
        }


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

        public override async Task CreateAsync(ApplicationUser user)
        {
            string sql = @"INSERT INTO dbo.AspNetUsers
                                ( Id ,
                                  Email ,
                                  EmailConfirmed ,
                                  PasswordHash ,
                                  SecurityStamp ,
                                  PhoneNumber ,
                                  PhoneNumberConfirmed ,
                                  TwoFactorEnabled ,
                                  LockoutEndDateUtc ,
                                  LockoutEnabled ,
                                  AccessFailedCount ,
                                  UserName
                                )
                        VALUES  ( @Id , -- Id - nvarchar(128)
                                  @Email , -- Email - nvarchar(256)
                                  @EmailConfirmed , -- EmailConfirmed - bit
                                  @PasswordHash , -- PasswordHash - nvarchar(max)
                                  @SecurityStamp , -- SecurityStamp - nvarchar(max)
                                  @PhoneNumber , -- PhoneNumber - nvarchar(max)
                                  @PhoneNumberConfirmed , -- PhoneNumberConfirmed - bit
                                  @TwoFactorEnabled , -- TwoFactorEnabled - bit
                                  GETDATE() , -- LockoutEndDateUtc - datetime
                                  @LockoutEnabled , -- LockoutEnabled - bit
                                  @AccessFailedCount , -- AccessFailedCount - int
                                  @UserName  -- UserName - nvarchar(256)
                                )";

            int count;
            using (var conn = DbManager.GetConnection())
            {
                count= conn.ExecuteAsync(sql, new
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    UserName = user.UserName
                }).Result;
            }

            //return base.CreateAsync(user);
        }
    }
}