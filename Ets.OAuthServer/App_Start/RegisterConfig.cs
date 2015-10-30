using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Ets.OAuthServer.Bll.Bll;
using Ets.OAuthServer.Bll.IBll;
using Ets.OAuthServer.Core.Infrastructure;
using Ets.OAuthServer.Core.Infrastructure.DependencyManagement;
using Ets.OAuthServer.Dal.Dal;
using Ets.OAuthServer.Dal.IDal;

namespace Ets.OAuthServer.App_Start
{
    public class RegisterConfig : IDependencyRegister
    {
        public int Order { get; private set; }
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            builder.RegisterType<AuthInfoDal>().AsImplementedInterfaces();
            builder.RegisterType<AuthInfoBll>().AsImplementedInterfaces();

            builder.RegisterType<ApplicationDal>().AsImplementedInterfaces();
            builder.RegisterType<ApplicationBill>().AsImplementedInterfaces();
        }
    }
}