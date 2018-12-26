using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MercuryLogger;

namespace Tracker.TimerControll
{
    class TimerController : ITimerController, IDisposable
    {
        public event EventHandler Tick;
        Timer timer;

        public TimerController(int period)
        {
            timer = new Timer(tick,null, period * 1000, period * 1000);
        }
        private void tick(object state)
        {
            MainLogger.GetInstance().Log("[Info] Timer tick " + timer.GetHashCode());
            Tick.Invoke(this, null);
        }

        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
        }
    }
}
