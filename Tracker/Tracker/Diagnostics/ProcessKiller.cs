using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracker.Diagnostics
{
    static class ProcessKiller
    {
        public static void CheckOnInstanse()
        {
            try
            {
                Process[] p = Process.GetProcessesByName("Tracker");
                Process p1 = Process.GetCurrentProcess();
                if (p.Length > 1)
                    foreach (var item in p)
                    {
                        if (item.Id == p1.Id)
                        {
                            NLog.LogManager.GetCurrentClassLogger().Fatal("Process " + item.ProcessName + " already running");
                            item.Kill();                            
                        }
                    }
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Fatal("ProcessKiller error: " + e);
                throw;
            }
           
        }
    }
}
