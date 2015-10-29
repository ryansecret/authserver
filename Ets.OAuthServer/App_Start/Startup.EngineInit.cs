using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Ets.OAuthServer.Core.Infrastructure;
using log4net.Config;

namespace Ets.OAuthServer
{
    public partial class Startup
    {
        public void EngineInit()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            var dependencyResolver = new HsrDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);
            EngineContext.Initialize(false);

            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log4Net.config");
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(path));
        }
    }
}