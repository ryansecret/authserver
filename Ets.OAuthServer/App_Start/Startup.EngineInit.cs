// ryan
// 201511123:16 PM

#region

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using Ets.OAuthServer.Core.Infrastructure;
using log4net.Config;

#endregion

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

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4Net.config");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(path));
        }
    }
}