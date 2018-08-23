using System;
using Microsoft.Extensions.Logging;

namespace TrackerServer
{
    public static class FileLoggerExtention
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));

            return factory;
        }
        public static void LogDebugLogin(this ILogger logger, string sessionKey)
        {
            logger.LogDebug("[" + DateTime.Now.ToString() + "]" + $"SessionKey:{sessionKey}");
        }
        public static void LogDebugSetTrackingValue(this ILogger logger, string sessionKey,int trackingValue)
        {
            logger.LogDebug("[" + DateTime.Now.ToString() + "]" + $"SessionKey:{sessionKey}" + $" TrackingValue: {trackingValue}");
        }
        public static void LogDebugSetPingError(this ILogger logger, string sessionKey,int pingError)
        {
            logger.LogDebug("[" + DateTime.Now.ToString() + "]" + $"SessionKey:{sessionKey}" + $" PingError: {pingError}");
        }


    }
}