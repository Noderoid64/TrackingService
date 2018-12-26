using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Diagnostics;
using Tracker.Model;
using Tracker.UIModule;
using Tracker.Net;
using Tracker.TimerControll;
using Tracker.Hook;

using MercuryLogger;


namespace Tracker
{
    internal class Core
    {
        #region Data
        IViewModule viewModule;
        INetModule netModule;
        ITimerController timer;        
        #endregion

        public Core()
        {
            
            ProcessKiller.CheckOnInstanse();
            LoggerController.ConfigureLogger();
            SettingsLoader.ReadConfig();

            viewModule = new ViewModule();
            viewModule.LogIn += ButtonLogIn;
            viewModule.ContinueSession += ButtonAddSession;

            netModule = new NetModule(GlobalSettings.GetInstance().PingTimeout);

            viewModule.ShowWindowLoginUI();


            HookController.Subscribe();

        }

        public void ButtonLogIn(object sender, WindowLoginEventArgs windowLoginEventArgs)
        {

        }
        public void ButtonAddSession(object sender, AdditionalSessionUiEventArgs additionalSessionUiEventArgs)
        {

        }
        public void Tick(object sender, EventArgs e)
        {
            GlobalSettings settings = GlobalSettings.GetInstance();
            if (netModule.SendPing(settings.PingUrl))
            {
                if(settings.ClientRequest.PingError==0)
                {
                    netModule.SendValue(settings.ServerUrl,settings.ClientRequest);
                }
                else
                {
                    netModule.SendPingError(settings.ServerUrl, settings.ClientRequest);
                }
            }
            else
            {
                if(settings.ClientRequest.PingError==0)
                {
                    //HookUnsubscribe
                }
                    settings.ClientRequest.PingError++;                
            }
        }


        private void changeData()
        {
            GlobalSettings settings = GlobalSettings.GetInstance();
            if(settings.IsAdditional)
            {
                if(settings.ServerResponse.additional_time == 0)
                {
                    //timer.
                }
                else
                {

                }
            }
            else
            {
                if(settings.ServerResponse.session_time == 0)
                {

                }
                else
                {

                }
            }
        }
    }
}
