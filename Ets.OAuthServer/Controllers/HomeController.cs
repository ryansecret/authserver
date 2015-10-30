using System.Web.Mvc;
using Ets.OAuthServer.Bll.IBll;
namespace Ets.OAuthServer
{
    public class HomeController : Controller
    {
        public IAuthInfoBll _testBll;
        public HomeController(IAuthInfoBll testBll)
        {
            _testBll = testBll;
        }
        public ActionResult Index()
        {
           //return new ContentResult() { Content = _testBll.Test() };
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
