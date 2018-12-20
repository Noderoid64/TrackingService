using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loger;
using Tracker.Tools.ApplicationSettings;

namespace Tracker
{
    static class AppLoger
    {
        static Loger.Loger loger;

        static AppLoger()
        {
            if (ApplicationSetting.GetInstance().UseLogin)
                loger = new FileLoger();
        }

        public static void Log(string message, MessageType type = MessageType.Debug)
        {
            if(ApplicationSetting.GetInstance().UseLogin)
            loger.Log(message,type);
        }
    }
}
