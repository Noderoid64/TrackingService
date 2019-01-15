using System;

namespace Tracker.UIModule
{
    public class AdditionalSessionUiEventArgs : EventArgs
    {
        public bool IsAdditionalSession { get; private set; }
        public AdditionalSessionUiEventArgs(bool isAdditionalSession)
        {
            this.IsAdditionalSession = isAdditionalSession;
        }
    }

}
