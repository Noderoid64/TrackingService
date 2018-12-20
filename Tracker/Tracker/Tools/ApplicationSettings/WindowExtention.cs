using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tracker.Tools.ApplicationSettings
{
    public static class WindowExtention
    {
        public static void SetWindowPosition(this Window window)
        {
            window.Left = System.Windows.SystemParameters.PrimaryScreenWidth - window.Width - 60;
            window.Top = System.Windows.SystemParameters.PrimaryScreenHeight - window.Height - 60;
        }
        public static void SetApplicationSettings(this MainWindow window, string Path)
        {
            ApplicationSetting.FillSettings(Path);
        }
    }
}
