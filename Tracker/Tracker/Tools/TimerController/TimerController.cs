using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Tracker.Model;
using Tracker.Tools.PingModule;
using Tracker.Tools.ConnectionModule.WebAPI;
using Tracker.Tools.ApplicationSettings;
using System.Diagnostics;

namespace Tracker.Tools.TimerController
{
    class TimerController
    {
        private Timer timer;
        private GlobalHookController GHK;
        public event Action SessionClose;

        public TimerController(ServerResponse response)
        {
            GHK = new GlobalHookController();
            if (!GHK.State)
                GHK.SetHook();
            TimerCallback tm = new TimerCallback(Tick);            
            timer = new Timer(tm, null, response.tracking_time * 1000, response.tracking_time * 1000);
        }

        public void  Tick(object o)
        {
            ApplicationSetting settings = ApplicationSetting.GetInstance();
            if (PingController.GetPing(settings.PingURL))
            {
                IWebAPI WebAPI = new DefaultWebAPI(new PostSender.DefaultPostSender());
                ServerResponse response;
                if(settings.Session.PingError == 0)
                {
                    response = WebAPI.SendTrackingTime(settings.Session.SessionKey, GHK.TrackValue);
                    Debug.Print("SendTrackingTime: " + GHK.TrackValue);
                }
                else
                {
                    response = WebAPI.SendPingError(settings.Session.SessionKey,settings.Session.PingError);
                    Debug.Print("SendTrackingTime: " + settings.Session.PingError);
                    settings.Session.PingError = 0;
                    
                }
                if(response==null)
                {
                    timer.Dispose();
                    SessionClose.Invoke();
                    
                }
                else
                {
                    if (!GHK.State)
                        GHK.SetHook();
                    
                    timer.Change(response.tracking_time * 1000, response.tracking_time * 1000);
                }
                
            }
            else
            {
                settings.Session.PingError++;
            }
        }
    }
}
