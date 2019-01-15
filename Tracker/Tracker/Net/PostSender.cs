using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

using Newtonsoft.Json;

namespace Tracker.Net
{
    internal static class PostSender
    {
        private static string Send(string message, List<string> parameters)
        {
            try
            {
                string response = "";
                using (var webClient = new WebClient())
                {
                    foreach (var item in parameters)
                    {
                        string key = item.Split('=')[0];
                        string value = item.Split('=')[1];
                        webClient.QueryString.Add(key,value);
                    }
                    //wc.QueryString.Add("parameter1", "Hello world");
                    //wc.QueryString.Add("parameter2", "www.stopbyte.com");
                    //wc.QueryString.Add("parameter3", "parameter 3 value.");

                    var resp = webClient.UploadValues(message, "POST", webClient.QueryString);

                    // data here is optional, in case we recieve any string data back from the POST request.
                    response = UnicodeEncoding.UTF8.GetString(resp);

                    // response = webClient.DownloadString(message);
                }
                if (response == "")
                {
                    NLog.LogManager.GetCurrentClassLogger().Error("No response");
                }
                return response;
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                NLog.LogManager.GetCurrentClassLogger().Error("PostSender error (no response)\n" + errResp);
                throw new Exception("Нет ответа сервера");
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Fatal("PostSender error\n" + e);
                throw new Exception("Ошибка запроса");
            }

        }
        public static ServerResponse SendValue(string host, int trackingValue, string sessionKey)
        {
            if (host == "" || sessionKey == "")
                NLog.LogManager.GetCurrentClassLogger().Error("ValuePostSender: host=" + host + " trackingValue=" + trackingValue + " sessionKey=" + sessionKey);
            List<string> parameters = new List<string>();
            parameters.Add("session_key=" + sessionKey);
            parameters.Add("tracking_value=" + trackingValue);
            // string localString = Send(host, "session_key=" + sessionKey + "&tracking_value=" + trackingValue);
            string localString = Send(host, parameters);
            ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(localString);
            return sr;
        }
        public static ServerResponse SendError(string host, int pingError, string sessionKey)
        {
            if (host == "" || pingError == 0 || sessionKey == "")
                NLog.LogManager.GetCurrentClassLogger().Error("ValuePostSender: host=" + host + " ping_error=" + pingError + " sessionKet=" + sessionKey);
            List<string> parameters = new List<string>();
            parameters.Add("session_key=" + sessionKey);
            parameters.Add("ping_error=" + pingError);
            //string localString = Send(host, "session_key=" + sessionKey + "&ping_error=" + pingError);
            string localString = Send(host, parameters);
            ServerResponse sr = JsonConvert.DeserializeObject<ServerResponse>(localString);
            return sr;
        }
    }
}
