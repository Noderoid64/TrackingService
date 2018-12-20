using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;
using Tracker.Tools.PostSender;
using Tracker.Tools.ApplicationSettings;

using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Tracker.Tools.ConnectionModule.WebAPI
{
    class DefaultWebAPI : IWebAPI
    {
        private Session currentSession;
        private IPostSender postSender;

        public DefaultWebAPI(IPostSender postSender)
        {
            currentSession = ApplicationSetting.GetInstance().Session;
            this.postSender = postSender;
        }

        private ServerResponse SendMessage(NameValueCollection nvc)
        {
            string localString = postSender.FormMessage(ApplicationSetting.GetInstance().ServerURL, nvc);
            AppLoger.Log("Post Request : " + localString);
            string resp = postSender.Post(localString);
            AppLoger.Log("Post Response: " + resp);
            try
            {
                return JsonConvert.DeserializeObject<ServerResponse>(resp);
            }
            catch
            {
                return null;
            }
            
        }
        public ServerResponse LogIn(string session_key)
        {
            NameValueCollection NVC = new NameValueCollection();
            NVC.Add("session_key",session_key);
            return SendMessage(NVC);
        }

        public ServerResponse SendTrackingTime(string session_key, int tracking_value)
        {
            NameValueCollection NVC = new NameValueCollection();
            NVC.Add("session_key", session_key);
            NVC.Add("tracking_value", tracking_value.ToString());
            return SendMessage(NVC);
        }

        public ServerResponse SendPingError(string session_key, int ping_error)
        {
            NameValueCollection NVC = new NameValueCollection();
            NVC.Add("session_key", session_key);
            NVC.Add("ping_error", ping_error.ToString());
            return SendMessage(NVC);
        }
    }
}
