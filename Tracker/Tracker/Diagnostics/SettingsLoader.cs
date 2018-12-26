using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Tracker.Model;
using MercuryLogger;

namespace Tracker.Diagnostics
{
    internal static class SettingsLoader
    {
        public static void ReadConfig()
        {
            try
            {
                using (StreamReader sr = new StreamReader("./settings.ini"))
                {
                    GlobalSettings globalSettings = GlobalSettings.GetInstance();
                    while (!sr.EndOfStream)
                    {
                        string field = sr.ReadLine();
                        string value = field.Split('=')[1];
                        field = field.Split('=')[0];
                        
                        switch (field)
                        {
                            case "server_url":
                                {
                                    globalSettings.ServerUrl = value;
                                }
                                break;
                            case "ping_url":
                                {
                                    globalSettings.PingUrl = value;
                                }
                                break;
                            case "ping_time_out":
                                {
                                    globalSettings.PingTimeout = int.Parse(value);
                                }
                                break;
                            case "loger":
                                {
                                    globalSettings.LoggerActive = (bool.Parse(value));
                                }
                                break;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MainLogger.GetInstance().Log("[Warning] Config reading failed\n" + e);
                throw;
            }
            MainLogger.GetInstance().Log("[Info] Config read successfully\n");

        }
    }
}
