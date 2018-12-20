using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using Tracker.Tools.ApplicationSettings;

namespace Tracker.Tools.ApplicationDiagnostic
{
    static class AppDiagnostic
    {
        public static void CheckOnInstanse()
        {
            Process[] p = Process.GetProcessesByName("Tracker");
            Process p1 = Process.GetCurrentProcess();
            if(p.Length > 1)
            foreach (var item in p)
            {
                    if (item.Id == p1.Id)
                        item.Kill();
            }
        }
        public static void SendIconToTray(NotifyIcon noty)
        {
            if (ApplicationSetting.GetInstance().Icon == null)
            {
                noty.Visible = true;
                ApplicationSetting.GetInstance().Icon = noty;

                AppDomain.CurrentDomain.ProcessExit += UnloadIconFromTray;
            }
                

        }
        private static void UnloadIconFromTray(object sende, EventArgs e)
        {
            if (ApplicationSetting.GetInstance().Icon != null)
            {
                ((NotifyIcon)(ApplicationSetting.GetInstance().Icon)).Visible = false;
            }
        }
    }
}
