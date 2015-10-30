using Ets.OAuthServer.Bll.IBll;
using Ets.OAuthServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Bll.Bll
{
    public class ApplicationBill : IApplicationBill
    {
        Dal.IDal.IApplicationDal dal;
        public ApplicationBill(Dal.IDal.IApplicationDal dal)
        {
            this.dal = dal;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Model.Application Add(string name, string callback)
        {
            var application = new Application()
            {
                Name = name,
                CallbackUrl = callback,
                AppKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                AppSecret = Guid.NewGuid().ToString().Replace("-", "").ToLower()
            };
            

            if (application.Validate())
            {
                dal.Add(application);
                
            }
            return application;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool Update(Model.Application application)
        {
            if (application.Validate())
            {
                dal.Update(application);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Application FindById(int id)
        {
            return dal.FindById(id);
        }
        /// <summary>
        /// 根据KEY查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Model.Application FindByKey(string key)
        {
            return dal.FindByKey(key);
        }
        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.Application> List()
        {
            return dal.List();
        }
    }
}
