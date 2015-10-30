using Ets.OAuthServer;
 
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using Ets.OAuthServer.Core.Infrastructure.DependencyManagement;

namespace Ets.OAuthServer
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_EndRequest(object sender, EventArgs e)
        //{

        //    //dispose registered resources
        //    //we do not register AutofacRequestLifetimeHttpModule as IHttpModule 
        //    //because it disposes resources before this Application_EndRequest method is called
        //    //and in this case the code in Application_EndRequest of Global.asax will throw an exception
        //    AutofacRequestLifetimeHttpModule.ContextEndRequest(sender, e);
        //}
    }
}
