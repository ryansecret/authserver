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
    /// <summary>
    /// A simple database connection manager
    /// </summary>
    public class DbManager 
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// 创建者:杨力
        /// 创建日期:10/29/2015 17:13 PM
        /// 修改者:
        /// 修改时间:
        /// ----------------------------------------------------------------------------------------
        static readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>
        /// 数据库连接
        /// </returns>
        /// 创建者:杨力
        /// 创建日期:10/29/2015 17:13 PM
        /// 修改者:
        /// 修改时间:
        /// ----------------------------------------------------------------------------------------
        public static IDbConnection GetConnection()
        {
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }
    }

}
