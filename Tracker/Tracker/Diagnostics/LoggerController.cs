using System;

using NLog;

namespace Tracker.Diagnostics
{
    static class LoggerController
    {

        public static void ConfigureLogger()
        {
            try
            {
                var config = new NLog.Config.LoggingConfiguration();

                var logfile = new NLog.Targets.FileTarget("logfile") {FileName = "Logs/" + DateTime.Now.ToFileTime() + ".txt" };
                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

                NLog.LogManager.Configuration = config;                
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Fatal("Exception: " + e.Message);
            }
            

            LogManager.GetCurrentClassLogger().Info("Logger succsess");

        }
    }
}
