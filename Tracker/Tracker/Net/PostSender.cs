using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

using MercuryLogger;
using Newtonsoft.Json;

namespace Tracker.Net
{
   internal static class PostSender
    {
        private static string Send(string message)
        {
            try
            {
                string response = "";
                using (var webClient = new WebClient())
                {                    
                    response = webClient.DownloadString(message);
                }
                if (response == "")
                {
                    MainLogger.GetInstance().Log("[Error] no response");
                }
                return response;
            }
            catch (Exception e)
            {
                MainLogger.GetInstance().Log("[Fatal] PostSender error\n" + e);
                throw;
            }
                       
        }
        public static ServerResponse SendValue(string host, int trackingValue, string sessionKey)
        {
            if (host == "" || trackingValue == 0 || sessionKey == "")
                MainLogger.GetInstance().Log("[Error] ValuePostSender: host=" + host + " trackingValue=" + trackingValue + " sessionKet=" + sessionKey);
            string localString = Send(host + "?session_key=" + sessionKey + "&tracking_value=" + trackingValue);
            ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(localString);
            return sr;
        }
        public static ServerResponse SendError(string host, int pingError, string sessionKey)
        {
            if (host == "" || pingError == 0 || sessionKey == "")
                MainLogger.GetInstance().Log("[Error] ValuePostSender: host=" + host + " ping_error=" + pingError + " sessionKet=" + sessionKey);
            string localString = Send(host + "?session_key=" + sessionKey + "&ping_error=" + pingError);
            ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(localString);
            return sr;
        }
    }
}
