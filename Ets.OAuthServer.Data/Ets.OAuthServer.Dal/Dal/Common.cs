using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal.Dal
{
    public class Common
    {
        public IDbConnection GetConnection()
        {
            var con = new SqlConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };
            con.Open();
            return con;
        }
    }
}
