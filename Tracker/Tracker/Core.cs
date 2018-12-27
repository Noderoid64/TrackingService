﻿using System;
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
            viewModule.ShowWindowLoginUI();

            netModule = new NetModule(GlobalSettings.GetInstance().PingTimeout);
           
            timer = new TimerController();
            timer.Tick += Tick;

        }

        public void ButtonLogIn(object sender, WindowLoginEventArgs windowLoginEventArgs)
        {
            GlobalSettings globalSettings = GlobalSettings.GetInstance();
            if(!netModule.SendPing(globalSettings.PingUrl))
            {
                viewModule.SendErrorWindowLoginUI("Ошибка");                
            }
            globalSettings.ClientRequest.SessionKey = windowLoginEventArgs.Key;

            ServerResponse sr = netModule.SendValue(globalSettings.ServerUrl, globalSettings.ClientRequest, (a)=>viewModule.SendErrorWindowLoginUI(a));
            if(sr != null)
            {
                MainLogger.GetInstance().Log("[Info] message: sessionTime:" + sr.session_time + " tracking:" + sr.tracking_time + " additional:" + sr.additional_time);
                if (viewModule.isWindowLoginUIVisible)
                viewModule.HideWindowLoginUI();
                viewModule.TrayMessage("Вы авторизировались как " + windowLoginEventArgs.Key);
                globalSettings.ServerResponse = sr;
                HookController.Subscribe();
                timer.TimerStart(globalSettings.ServerResponse.tracking_time, globalSettings.ServerResponse.tracking_time);
            }
            else
            {
                MainLogger.GetInstance().Log("[Error] message: no resp");
                viewModule.TrayMessage("Проверьте подключиние или повторите попытку позже");
            }
               
            // if (viewModule.isWindowLoginUIVisible)
            //   viewModule.HideWindowLoginUI();
        }
        public void ButtonAddSession(object sender, AdditionalSessionUiEventArgs additionalSessionUiEventArgs)
        {

        }
        private void Tick(object sender, EventArgs e)
        {
            GlobalSettings settings = GlobalSettings.GetInstance();

            if (netModule.SendPing(settings.PingUrl))
            {
                if(settings.ClientRequest.PingError==0)
                {
                    ServerResponse response = netModule.SendValue(settings.ServerUrl,settings.ClientRequest);
                    if(response != null)
                    {
                        changeData(response);
                        if(GlobalSettings.GetInstance().ClientRequest.TrackingValue == 0)
                        {
                            HookController.Subscribe();
                        }
                        else
                        {
                            GlobalSettings.GetInstance().ClientRequest.TrackingValue = 0;
                        }
                    }
                    else
                    {
                        timer.TimerStop();
                        SessionClose();
                    }
                }
                else
                {
                   ServerResponse sr = netModule.SendPingError(settings.ServerUrl, settings.ClientRequest);
                    if(sr != null)
                    {
                        changeData(sr);
                    }
                    else
                    {
                        timer.TimerStop();
                        SessionClose();
                        return;
                    }
                }
            }
            else
            {
                if(settings.ClientRequest.PingError==0)
                {
                    //HookUnsubscribe
                    HookController.Unsubscribe();
                }
                    settings.ClientRequest.PingError++;                
            }
        }


        private void changeData(ServerResponse sr)
        {
            GlobalSettings settings = GlobalSettings.GetInstance();
            settings.ServerResponse = sr;
            if(settings.IsAdditional)
            {
                if(settings.ServerResponse.additional_time == 0)
                {
                    timer.TimerStop();
                    //Session Close
                    SessionClose();
                    return;
                }
                else
                {
                    if(settings.ServerResponse.tracking_time > settings.ServerResponse.additional_time)
                    {
                        timer.TimerChange(settings.ServerResponse.additional_time,-1 );
                    }
                    return;
                }
            }
            else
            {
                if (settings.ServerResponse.session_time == 0)
                {
                    MainLogger.GetInstance().Log("stop");
                    timer.TimerStop();
                    if(settings.ServerResponse.additional_time ==0)
                    {
                        //SessionClose
                        SessionClose();
                    }
                    else
                    {
                        //ProposeAddition
                        ProposeAddition();
                    }
                    return;
                }
                else
                {
                    if(settings.ServerResponse.tracking_time > settings.ServerResponse.session_time)
                    {
                        timer.TimerChange(settings.ServerResponse.session_time,-1);
                    }
                    return;
                }
            }
        }
        private void SessionClose()
        {
            MainLogger.GetInstance().Log("[Info] SessionClose");
            GlobalSettings.GetInstance().ClientRequest = new ClientRequest();
            viewModule.ShowWindowLoginUI();
            viewModule.TrayMessage("Сессия завершена");
        }
        private void ProposeAddition()
        {
            MainLogger.GetInstance().Log("[Info] ProposeAddition");
            viewModule.ShowAdditionalSessionUI();

            viewModule.TrayMessage("Сессия завершена");
        }
    }
}
