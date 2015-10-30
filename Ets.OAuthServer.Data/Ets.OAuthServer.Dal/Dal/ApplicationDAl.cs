using Ets.OAuthServer.Dal.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Ets.OAuthServer.Dal.Dal
{
    public class ApplicationDal : IApplicationDal
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="application"></param>
        public void Add(Model.Application application)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = ApplicationDalSql.InsertSql();

                int id = conn.ExecuteScalar<int>(sql, application);
                application.Id = id;
            }
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="application"></param>
        public void Update(Model.Application application)
        {
            using (var conn = Database.GetConnection())
            {
                var sqldata = ApplicationDalSql.Update();
                conn.Execute(sqldata, application);
            }
        }
        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Application FindById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                Tuple<string,dynamic> sqldata = ApplicationDalSql.FindById(id);

                return conn.Query<Model.Application>(sqldata.Item1, (object)sqldata.Item2).FirstOrDefault();
            }
        }
        /// <summary>
        /// 根据APPKey查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Model.Application FindByKey(string key)
        {
            using (var conn = Database.GetConnection())
            {
                Tuple<string, dynamic> sqldata = ApplicationDalSql.FindByKey(key);

                return conn.Query<Model.Application>(sqldata.Item1, (object)sqldata.Item2).FirstOrDefault();
            }
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.Application> List()
        {
            using (var conn = Database.GetConnection())
            {
                var sql = ApplicationDalSql.List();
                return conn.Query<Model.Application>(sql);
            }
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="application"></param>
        public void Delete(Model.Application application)
        {
            using (var conn = Database.GetConnection())
            {
                var sqldata = ApplicationDalSql.Delete(application.Id);
                conn.Execute(sqldata.Item1, (object)sqldata.Item2);
            }
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DeleteAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Execute(ApplicationDalSql.DeleteAll());
            }
        }
    }
}
