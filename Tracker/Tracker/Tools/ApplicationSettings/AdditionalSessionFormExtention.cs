using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Tools.ApplicationSettings
{
    public static class AdditionalSessionFormExtention
    {
        public static void SetWindowPosition(this AdditionalSessionForm window, string Path)
        {
            window.Left = System.Windows.SystemParameters.PrimaryScreenWidth - window.Width - 30;
            window.Top = System.Windows.SystemParameters.PrimaryScreenHeight - window.Height - 30;
        }
    }
}
