using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.UIModule
{
    internal class ViewModule : IViewModule
    {
        private TrayModule trayModule;
        private WindowLoginUI windowLogin;
        private AdditionalSessionUI additionalSessionUI;

        public ViewModule()
        {
            trayModule = new TrayModule();
            windowLogin = new WindowLoginUI();
            additionalSessionUI = new AdditionalSessionUI();
        }

        public bool isWindowLoginUIVisible { get; private set; }
        public bool IsAdditionalSessionUIVisible { get; private set; }

        public event EventHandler<AdditionalSessionUiEventArgs> ContinueSession;
        public event EventHandler<WindowLoginEventArgs> LogIn;

        public void HideAdditionalSessionUI()
        {
            throw new NotImplementedException();
        }
        public void HideWindowLoginUI()
        {
            windowLogin.Hide();
            isWindowLoginUIVisible = false;
        }

        public void SendErrorAdditionalSessionUI(string message)
        {
            throw new NotImplementedException();
        }

        public void SendErrorWindowLoginUI(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowAdditionalSessionUI()
        {
            throw new NotImplementedException();
        }
        public void ShowWindowLoginUI()
        {
            windowLogin.Show();
            isWindowLoginUIVisible = true;
        }
    }
}
