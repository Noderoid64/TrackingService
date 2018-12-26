using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MercuryLogger;
using MercuryLogger.Loggers;
using MercuryLogger.Extentions;
using MercuryLogger.Diagnostics;

namespace Tracker.Diagnostics
{
    static class LoggerController
    {
        static Object locker = new Object();

        public static void ConfigureLogger()
        {
            try
            {
                Logger localLogger = MainLogger.GetInstance();
                TimeExtention timeExtention = new TimeExtention()
                {
                    ShowHour = true,
                    ShowMinute = true,
                    ShowSecond = true,
                    ShowMillisecond = true
                };
                FileLogger fileLogger = new FileLogger("./Logs/");
                timeExtention.AddLogger(fileLogger);
                localLogger.AddLogger(timeExtention);
                localLogger.Log("          <--NewLog-->");
            }
            catch(Exception e)
            {
                LogFatalBeforeLoading("Configure Logger error: \n" + e);
            }
            
        }
       

        public static void LogFatalBeforeLoading(string message)
        {
            lock(locker)
            {
                FileLogger fileLogger = new FileLogger("./Logs/","Fatal" + DateTime.Now.ToFileTime().ToString() + ".txt");
                DebugLevelExtention debugLevelExtention = new DebugLevelExtention(DebugLevelExtention.Levels.Fatal);
                TimeExtention timeExtention = new TimeExtention()
                {
                    ShowHour = true,
                    ShowMinute = true,
                    ShowSecond = true,
                    ShowMillisecond = true
                };
                debugLevelExtention.AddLogger(fileLogger);
                timeExtention.AddLogger(debugLevelExtention);
                timeExtention.Log(message);
            }
        }
    }
}
