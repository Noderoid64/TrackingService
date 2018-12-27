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

        public void TimerStart(int dueTime, int period)
        {

            timer = new Timer(tick, null, dueTime * 1000, period * 1000);
            MainLogger.GetInstance().Log("[Info] Timer (" + timer.GetHashCode() + ") Start");
        }
        public void TimerStop()
        {
            MainLogger.GetInstance().Log("[Info] Timer (" + timer.GetHashCode() + ") Stop");
            Dispose();
        }
        public void TimerChange(int dueTime, int period)
        {
            MainLogger.GetInstance().Log("[Info] Timer (" + timer.GetHashCode() + ") Change: dutTime: " + dueTime + " period: " + period);
            if (period == -1)
                timer.Change(dueTime * 1000, Timeout.Infinite);
            else
                timer.Change(dueTime * 1000, period * 1000);
        }

        private void tick(object state)
        {
            MainLogger.GetInstance().Log("[Info] Timer (" + timer.GetHashCode() + " Tick");
            Tick.Invoke(this, null);
        }

        public void Dispose()
        {
            
            if (timer != null)
            {
                MainLogger.GetInstance().Log("[Info] Timer (" + timer.GetHashCode() + ") Dispose");
                timer.Dispose();
            }
                
        }

        
    }
}
