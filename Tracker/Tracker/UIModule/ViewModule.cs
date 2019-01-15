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

            additionalSessionUI.SessionContinue += AdditionalSessionContinue;
        }

        public bool isWindowLoginUIVisible { get; private set; }
        public bool IsAdditionalSessionUIVisible { get; private set; }

        public event EventHandler<AdditionalSessionUiEventArgs> ContinueSession;
        public event EventHandler<WindowLoginEventArgs> LogIn;

        public void HideAdditionalSessionUI()
        {
            Action A = () =>
            {
                additionalSessionUI.Hide();
                IsAdditionalSessionUIVisible = false;
            };
            additionalSessionUI.Dispatcher.BeginInvoke(A);
            
        }
        public void HideWindowLoginUI()
        {
            Action A = () =>
            {
                windowLogin.Hide();
                isWindowLoginUIVisible = false;
            };
            windowLogin.Dispatcher.BeginInvoke(A);
            
        }

        public void SendErrorAdditionalSessionUI(string message)
        {
            throw new NotImplementedException();
        }
        public void SendErrorWindowLoginUI(string message)
        {
            windowLogin.SetError(message);
        }

        public void ShowAdditionalSessionUI(TimeSpan time)
        {
            Action A = () =>
            {
                additionalSessionUI.SetTime(time);
                additionalSessionUI.StartTimer();
                additionalSessionUI.Show();
            };
            additionalSessionUI.Dispatcher.BeginInvoke(A);
            
            
            IsAdditionalSessionUIVisible = true;
        }
        public void ShowWindowLoginUI()
        {
            Action A = () =>
            {
                windowLogin.Show();
                isWindowLoginUIVisible = true;
            };
            windowLogin.Dispatcher.BeginInvoke(A);

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
        private void AdditionalSessionContinue(object sender, AdditionalSessionUiEventArgs e)
        {
            ContinueSession.Invoke(sender,e);
        }
    }
}
