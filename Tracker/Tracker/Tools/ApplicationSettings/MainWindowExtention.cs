using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Tools.ApplicationSettings
{
    public static class MainWindowExtention
    {
        public static void SetWindowPosition(this MainWindow window)
        {
            window.Left = System.Windows.SystemParameters.PrimaryScreenWidth - window.Width - 30;
            window.Top = System.Windows.SystemParameters.PrimaryScreenHeight - window.Height - 30;
        }
        public static void SetApplicationSettings(this MainWindow window, string Path)
        {
            ApplicationSetting.FillSettings(Path);
        }
    }
}
