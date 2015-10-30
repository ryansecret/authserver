using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Bll.IBll
{
    public interface IApplicationBill
    {
        Model.Application Add(string name, string callback);
        bool Update(Model.Application application);
        Model.Application FindById(int id);
        Model.Application FindByKey(string key);
        IEnumerable<Model.Application> List();
    }
}
