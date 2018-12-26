using System;

namespace Tracker.UIModule
{
    internal class AdditionalSessionUiEventArgs : EventArgs
    {
        public bool IsAdditionalSession { get; private set; }
        public AdditionalSessionUiEventArgs(bool isAdditionalSession)
        {
            IsAdditionalSession = IsAdditionalSession;
        }
    }

}
