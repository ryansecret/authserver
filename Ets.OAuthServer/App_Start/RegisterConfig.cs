// ryan
// 201511123:16 PM

#region

using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using Ets.OAuthServer.Bll.Bll;
using Ets.OAuthServer.Core.Infrastructure;
using Ets.OAuthServer.Core.Infrastructure.DependencyManagement;
using Ets.OAuthServer.Dal.Dal;
using LibLog.Example.Library.Logging;

#endregion

namespace Ets.OAuthServer.App_Start
{
    public class RegisterConfig : IDependencyRegister
    {
        public int Order { get; private set; }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterInstance(LogProvider.GetLogger("Logger_Error")).Named<ILog>("Error").SingleInstance();
            builder.RegisterInstance(LogProvider.GetLogger("Logger_Info")).Named<ILog>("Info").SingleInstance();
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray()).PropertiesAutowired();

            builder.RegisterType<AuthInfoDal>().AsImplementedInterfaces();
            builder.RegisterType<AuthInfoBll>().AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<ApplicationDal>().AsImplementedInterfaces();
            builder.RegisterType<ApplicationBill>().AsImplementedInterfaces();
        }
    }
}