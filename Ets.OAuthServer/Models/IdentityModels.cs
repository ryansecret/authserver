using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

using Dapper.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;
using AspnetIdentity.Dapper;

namespace Ets.OAuthServer
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   [Table("AspNetUsers")] 
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

   public class ApplicationDbContext : DapperIdentityDbContext<ApplicationUser, IdentityRole>
   {
       public ApplicationDbContext(IDbConnection connection)
           : base(connection)
       {
       }

       public static ApplicationDbContext Create()
       {
           var connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
           var conn = new SqlConnection(connString);
           return new ApplicationDbContext(conn);
       }
   }
}