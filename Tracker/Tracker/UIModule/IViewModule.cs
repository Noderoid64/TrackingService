using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.UIModule
{
    interface IViewModule
    {
        event EventHandler<WindowLoginEventArgs> LogIn;
        event EventHandler<AdditionalSessionUiEventArgs> ContinueSession;

        void ShowWindowLoginUI();
        void ShowAdditionalSessionUI(TimeSpan time);
        void HideWindowLoginUI();
        void HideAdditionalSessionUI();

        bool isWindowLoginUIVisible { get;}
        bool IsAdditionalSessionUIVisible { get; }

        void SendErrorWindowLoginUI(string message);
        void SendErrorAdditionalSessionUI(string message);

        void TrayMessage(string message);
    }
}
