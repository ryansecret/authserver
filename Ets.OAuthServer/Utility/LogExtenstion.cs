using System;
using LibLog.Example.Library.Logging;

namespace Ets.OAuthServer.Utility
{
    public static class LogExtenstion
    {
        public static void LogError(this Exception exception)
        {
            LogProvider.GetLogger("Logger_Error").Error(exception.StackTrace);
        }

        public static void LogInfo(this string info)
        {
            LogProvider.GetLogger("Logger_Info").Error(info);
        }
    }
}
