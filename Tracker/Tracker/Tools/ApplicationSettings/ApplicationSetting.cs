using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tracker.Model;

namespace Tracker.Tools.ApplicationSettings
{
    class ApplicationSetting
    {
        #region singleton
        private ApplicationSetting() { }
        private static ApplicationSetting appSetting;

        public static ApplicationSetting GetInstance()
        {
            if (appSetting == null)
            {
                appSetting = new ApplicationSetting();
                appSetting.Session = new Session();
            }                
            return
                appSetting;
        }
        #endregion

        #region data
        public Session Session { get; set; }
        public string ServerURL { get; set; }
        public string PingURL { get; set; }
        #endregion

        public static void FillSettings(string FilePath)
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                Dictionary<string, string> settings = new Dictionary<string, string>();
                while (!sr.EndOfStream)
                {
                    string localString = sr.ReadLine();
                    settings.Add(localString.Split('=')[0], localString.Split('=')[1]);
                }
                if (appSetting == null)
                    ApplicationSetting.GetInstance();
               appSetting.ServerURL = settings["server_url"];
               appSetting.PingURL = settings["ping_url"];
            }
        }
    }
}
