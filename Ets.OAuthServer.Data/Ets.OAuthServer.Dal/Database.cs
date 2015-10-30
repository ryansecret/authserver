using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal
{
    class Database
    {
        static readonly string CONN_STRING = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

        public static IDbConnection GetConnection()
        {
            IDbConnection conn = new SqlConnection(CONN_STRING);
            conn.Open();
            return conn;
        }
    }
}
