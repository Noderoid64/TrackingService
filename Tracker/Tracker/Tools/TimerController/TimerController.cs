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
        public event Action<bool> SessionClose;

        public TimerController(ServerResponse response)
        {
            GHK = new GlobalHookController();
            if (!GHK.State)
                GHK.SetHook();
            TimerCallback tm = new TimerCallback(Tick);
            timer = new Timer(tm, response.tracking_time, response.tracking_time * 1000, response.tracking_time * 1000);
            AppLoger.Log("Timer start " + timer.GetHashCode().ToString());
        }

        public void Tick(object o)
        {
            AppLoger.Log("Timer tick " + timer.GetHashCode().ToString());
            int trackingTime = (int)o;

            ApplicationSetting settings = ApplicationSetting.GetInstance();

            if (trackingTime > int.Parse(settings.PingTimeOut))
                trackingTime = int.Parse(settings.PingTimeOut);

            if (PingController.GetPing(settings.PingURL, trackingTime) && timer != null)
            {
                IWebAPI WebAPI = new DefaultWebAPI(new PostSender.DefaultPostSender());
                ServerResponse response;
                if (settings.Session.PingError == 0)
                {
                    response = WebAPI.SendTrackingTime(settings.Session.SessionKey, GHK.TrackValue);
                    Debug.Print("SendTrackingTime: " + GHK.TrackValue);
                }
                else
                {
                    response = WebAPI.SendPingError(settings.Session.SessionKey, settings.Session.PingError);
                    Debug.Print("SendTrackingTime: " + settings.Session.PingError);
                    settings.Session.PingError = 0;

                }
                if (response == null)
                {
                    timer.Dispose();
                    SessionClose.Invoke(false);
                    AppLoger.Log("Timer stop " + timer.GetHashCode().ToString());
                }
                else
                {
                    if (!GHK.State)
                        GHK.SetHook();

                    if (!ApplicationSetting.GetInstance().AdditionalTime)
                    {
                        if (response.tracking_time > response.session_time)
                        {
                            if (response.session_time <= 0)
                            {
                                timer.Change(0, Timeout.Infinite);
                                timer.Dispose();
                                if (response.additional_time == 0)
                                    SessionClose.Invoke(false);
                                else
                                    SessionClose.Invoke(true);

                                AppLoger.Log("Timer stop " + timer.GetHashCode().ToString());
                                if (ApplicationSetting.GetInstance().AdditionalTime == true)
                                    ApplicationSetting.GetInstance().AdditionalTime = false;
                                else
                                    ApplicationSetting.GetInstance().AdditionalTime = true;
                            }
                            else
                            {
                                AppLoger.Log("Last request");
                                timer?.Change(response.session_time * 1000, response.tracking_time * 1000);
                            }


                        }
                        else
                        {
                            try
                            {
                                timer?.Change(response.tracking_time * 1000, response.tracking_time * 1000);
                                settings.Session.AdditionalTime = response.additional_time;
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        if (response.tracking_time > response.additional_time)
                        {
                            if (response.additional_time <= 0)
                            {
                                timer.Change(0, Timeout.Infinite);
                                timer.Dispose();
                                SessionClose.Invoke(false);
                                AppLoger.Log("Timer stop " + timer.GetHashCode().ToString());
                                if (ApplicationSetting.GetInstance().AdditionalTime == true)
                                    ApplicationSetting.GetInstance().AdditionalTime = false;
                                else
                                    ApplicationSetting.GetInstance().AdditionalTime = true;
                            }
                            else
                                timer?.Change(response.additional_time * 1000, response.tracking_time * 1000);


                        }
                        else
                        {
                            try
                            {
                                timer?.Change(response.tracking_time * 1000, response.tracking_time * 1000);
                                settings.Session.AdditionalTime = response.additional_time;
                            }
                            catch
                            {

                            }

                        }
                    }





                }

            }
            else
            {
                settings.Session.PingError++;
            }
        }
    }
}
