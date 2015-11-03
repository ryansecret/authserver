using System;
using Ets.OAuthServer.Core.Infrastructure;
using LibLog.Example.Library.Logging;

namespace Ets.OAuthServer.Utility
{
    public static class LogExtenstion
    {
        public static void LogError(this Exception exception)
        {
            EngineContext.Current.ContainerManager.Resolve<ILog>("Error").Error(exception.StackTrace);
        }

        public static void LogInfo(this string info)
        {
            EngineContext.Current.ContainerManager.Resolve<ILog>("Info").Error(info);
        }
    }
}
