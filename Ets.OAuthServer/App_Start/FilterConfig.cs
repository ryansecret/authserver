using System.Web.Mvc;

namespace Ets.OAuthServer
{
    using Ets.OAuthServer.Utility;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}