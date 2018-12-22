using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Diagnostics;
using Tracker.UIModule;

namespace Tracker
{
    internal class Core
    {
        #region Data
        TrayModule trayModule;
        #endregion

        public Core()
        {
            
            ProcessKiller.CheckOnInstanse();
            LoggerController.ConfigureLogger();
            trayModule = new TrayModule();
        }
    }
}
