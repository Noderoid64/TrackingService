using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Tracker.Tools.TimerController
{
    class GlobalHookController
    {
        private int trackValue = 1;
        private bool state = false;

        #region gets
        public int TrackValue
        {
            get
            {
                return trackValue;
            }
        }
        public bool State
        {
            get
            {
                return state;
            }
        }
        #endregion

        private GlobalHook hook;
        public GlobalHookController()
        {
            hook = new GlobalHook();
        }
        public void SetHook()
        {
         //   Debug.Print("SetHook start " + state);
            lock (hook)
            {
                if (!state)
                {
                    trackValue = 1;
                    hook.KeyDown += EventHandler;
                    hook.MouseButtonDown += EventHandler;
                    hook.MouseMove += EventHandler;
                    state = true;
                }
                else
                {
                    throw new Exception();
                }
            }
          //  Debug.Print("SetHook end " + state);


        }
        public void EventHandler(object sender, EventArgs e)
        {
          //  Debug.Print("UnHook start " + state);
            lock (hook)
            {
                trackValue = 0;
                hook.KeyDown -= EventHandler;
                hook.MouseButtonDown -= EventHandler;
                hook.MouseMove -= EventHandler;
                state = false;
            }
           // Debug.Print("UnHook end " + state);

        }

    }
}
