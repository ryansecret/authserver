using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;

namespace Ets.OAuthServer.Core.Infrastructure.DependencyManagement
{
    public class AtuofacRequestLifetimeOwin
    {
        public static readonly object HttpRequestTag = "AutofacWebRequest";
        public static ILifetimeScope GetLifetimeScope(ILifetimeScope container,
         Action<ContainerBuilder> configurationAction)
        {
            var key = "EtsAutofac";
            var lifeScope = HttpContext.Current.GetOwinContext().Get<ILifetimeScope>(key);
            if (lifeScope==null)
            {
                lifeScope = InitializeLifetimeScope(configurationAction, container);
                HttpContext.Current.GetOwinContext().Set(key, lifeScope);
            }
            return lifeScope;
        }
        private static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction,
           ILifetimeScope container)
        {
            return (configurationAction == null)
                ? container.BeginLifetimeScope(HttpRequestTag)
                : container.BeginLifetimeScope(HttpRequestTag, configurationAction);
        }
    }
}
