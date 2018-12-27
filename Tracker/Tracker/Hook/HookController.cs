﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

using MercuryLogger;

namespace Tracker.Hook
{
    class HookController : IDisposable
    {
        #region Singleton
        //private static HookController instance;
        //private static Object syncObj = new Object();        
        //private HookController()
        //{

        //}
        //public HookController GetInstance()
        //{

        //    lock (syncObj)
        //    {
        //        if (instance == null)
        //            instance = new HookController();
        //        return instance;
        //    }
            
        //}
        #endregion

        private static GlobalHook hook = new GlobalHook();

        public static bool state = false;
       
        public static void Subscribe()
        {
            if(state == false)
            {
                GlobalSettings.GetInstance().ClientRequest.TrackingValue = 1;
                MainLogger.GetInstance().Log("[Info] Hook subscribe");
                hook.MouseMove += UserActivity;
                hook.MouseButtonDown += UserActivity;
                hook.KeyDown += UserActivity;
                state = true;
            }
            else
            {
                //throw new Exception("GlobalHool state true, but you want to subscribe");
            }
        }
        public static void Unsubscribe()
        {
            if (state == true)
            {
                MainLogger.GetInstance().Log("[Info] Hook unsubscribe");
                hook.MouseMove -= UserActivity;
                hook.MouseButtonDown -= UserActivity;
                hook.KeyDown -= UserActivity;
                state = false;
            }
            else
            {
               // throw new Exception("GlobalHool state false, but you want to unsubscribe");
            }
        }

        private static void UserActivity(object sender, EventArgs e)
        {
            hook.MouseMove -= UserActivity;
            hook.MouseButtonDown -= UserActivity;
            hook.KeyDown -= UserActivity;
            state = false;

            GlobalSettings.GetInstance().ClientRequest.TrackingValue = 0;

            MainLogger.GetInstance().Log("[Info] UserActivity");
        }

        public void Dispose()
        {
            if (hook != null)
                hook.Dispose();
        }
    }
}