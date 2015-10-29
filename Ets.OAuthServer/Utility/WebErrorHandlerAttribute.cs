using System.Web.Mvc;

namespace Ets.OAuthServer.Utility
{
    public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (context.ExceptionHandled)
                return;
            
            context.Exception.LogError();
        }
    }
}
