using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.TimerControll
{
    interface ITimerController
    {
        event EventHandler Tick;

        void TimerStart(int dueTime, int period);
        void TimerStop();
        void TimerChange(int dueTime, int period);
    }
}
