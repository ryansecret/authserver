using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal.Dal
{
    /// <summary>
    /// Applcation sql 管理
    /// </summary>
    public static class ApplicationDalSql
    {
        /// <summary>
        /// 插入SQL
        /// </summary>
        /// <returns></returns>
        public static string InsertSql()
        {
            return @"INSERT INTO Application (Name,AppKey,AppSecret,CallbackUrl)
                    VALUES (@Name,@AppKey,@AppSecret,@CallbackUrl);SELECT SCOPE_IDENTITY()";
        }
        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <returns></returns>
        private static string BaseSelectSql()
        {
            return "SELECT Id,Name,AppKey,AppSecret,CallbackUrl FROM Application";
        }
        /// <summary>
        /// 列表查询SQL
        /// </summary>
        /// <returns></returns>
        public static string List()
        {
            string sql = string.Join(" ", BaseSelectSql(), " ORDER BY id DESC");
            return sql;
        }
        /// <summary>
        /// 更新SQL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Update()
        {
            string sql = @"UPDATE Application
                           SET AppKey = @AppKey
                              ,AppSecret = @AppSecret
                              ,CallbackUrl = @CallbackUrl
                              ,Name = @Name
                         WHERE Id=@Id";

            return sql;
        }

        public static Tuple<string, dynamic> FindByKey(string key)
        {
             string sql = string.Join(" ", BaseSelectSql(), "WHERE AppKey=@AppKey");
            var data = new { AppKey = key };

            return new Tuple<string, dynamic>(sql, data);
        }

        /// <summary>
        /// 根据ID查询SQL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Tuple<String, dynamic> FindById(int id)
        {
            string sql = string.Join(" ", BaseSelectSql(), "WHERE Id=@Id");
            var data = new { Id = id };

            return new Tuple<string, dynamic>(sql, data);
        }
        /// <summary>
        /// 删除一条记录SQL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Tuple<String, dynamic> Delete(int id)
        {
            string sql = string.Join(" ", DeleteAll(), "WHERE Id=@Id");
            var data = new { Id = id };

            return new Tuple<string, dynamic>(sql, data);
        }

        public static string DeleteAll()
        {
            return "DELETE FROM Application";
        }

    }
}
