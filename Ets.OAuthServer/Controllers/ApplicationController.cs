using Ets.OAuthServer.Bll.IBll;
using Ets.OAuthServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ets.OAuthServer.Controllers
{
    public class ApplicationController : Controller
    {
        IApplicationBill bill;
        public ApplicationController(IApplicationBill bill)
        {
            this.bill = bill;
        }
        public ActionResult Index()
        {
            ViewBag.AddApplication = new Application();

            var list =  bill.List();
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(string name, string callbackurl)
        {
            Application application = bill.Add(name, callbackurl);

            if (application.GetBrokenRules().Count == 0)
            {
                ViewBag.AddApplication = new Application();
            }
            else
            {
                ViewBag.AddApplication = application;
            }

            var list = bill.List();
            return View(list);
        }
    }
}