using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker.UIModule
{
    internal class TrayModule : IDisposable
    {
        private NotifyIcon icon;
        public TrayModule()
        {
            icon = new NotifyIcon();
            SetIcon();
            icon.Visible = true;
        }

        public void Dispose()
        {
            if (icon != null)
                icon.Dispose();
        }

        private void SetIcon()
        {
            icon.Icon = new System.Drawing.Icon("./icon.ico");
        }
    }
}
