using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

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

            windowLogin.ButtonLoginClick += LogInButton;
        }

        public bool isWindowLoginUIVisible { get; private set; }
        public bool IsAdditionalSessionUIVisible { get; private set; }

        public event EventHandler<AdditionalSessionUiEventArgs> ContinueSession;
        public event EventHandler<WindowLoginEventArgs> LogIn;

        public void HideAdditionalSessionUI()
        {
            additionalSessionUI.Hide();
            IsAdditionalSessionUIVisible = false;
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
            windowLogin.SetError(message);
        }

        public void ShowAdditionalSessionUI()
        {
            Action A = () =>
            {
                additionalSessionUI.Show();
            };
            additionalSessionUI.Dispatcher.BeginInvoke(A);
            
            
            IsAdditionalSessionUIVisible = true;
        }
        public void ShowWindowLoginUI()
        {
            windowLogin.Show();
            isWindowLoginUIVisible = true;
        }

        public void TrayMessage(string message)
        {
            trayModule.ShowMessage(message);
        }

        private void LogInButton(object sender, WindowLoginEventArgs e)
        {
            LogIn.Invoke(sender, e);
        }
    }
}
