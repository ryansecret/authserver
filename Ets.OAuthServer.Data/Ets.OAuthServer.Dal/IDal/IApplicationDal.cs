using Ets.OAuthServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal.IDal
{
    public interface IApplicationDal
    {
        void Add(Application application);
        void Update(Application application);
        Application FindById(int id);
        Application FindByKey(string key);
        IEnumerable<Model.Application> List();
        void Delete(Application application);
        void DeleteAll();
    }
}
