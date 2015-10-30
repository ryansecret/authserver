using Ets.OAuthServer.Bll.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Bll.Bll
{
    class ApplicationBll : IApplicationBill
    {
        Dal.IDal.IApplicationDal dal;
        public ApplicationBll(Dal.IDal.IApplicationDal dal)
        {
            this.dal = dal;
        }

        public bool Add(Model.Application application)
        {
            if (application.Validate())
            {
                dal.Add(application);
                return true;
            }
            return false;
        }

        public bool Update(Model.Application application)
        {
            if (application.Validate())
            {
                dal.Update(application);
                return true;
            }
            return false;
        }

        public Model.Application FindById(int id)
        {
            return dal.FindById(id);
        }

        public Model.Application FindByKey(string key)
        {
            return dal.FindByKey(key);
        }

        public IEnumerable<Model.Application> List()
        {
            return dal.List();
        }
    }
}
