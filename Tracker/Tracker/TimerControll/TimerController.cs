using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace Tracker.TimerControll
{
    class TimerController : ITimerController, IDisposable
    {
        public event EventHandler Tick;
        Timer timer;

        public void TimerStart(int dueTime, int period)
        {

            timer = new Timer(tick, null, dueTime * 1000, period * 1000);
            NLog.LogManager.GetCurrentClassLogger().Info("Timer (" + timer.GetHashCode() + ") Start");
        }
        public void TimerStop()
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Timer (" + timer.GetHashCode() + ") Stop");
            Dispose();
        }
        public void TimerChange(int dueTime, int period)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Timer (" + timer.GetHashCode() + ") Change: dutTime: " + dueTime + " period: " + period);
            if (period == -1)
                timer.Change(dueTime * 1000, Timeout.Infinite);
            else
                timer.Change(dueTime * 1000, period * 1000);
        }

        private void tick(object state)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Timer (" + timer.GetHashCode() + " Tick");
            Tick.Invoke(this, null);
        }

        public void Dispose()
        {
            
            if (timer != null)
            {
                NLog.LogManager.GetCurrentClassLogger().Info("Timer (" + timer.GetHashCode() + ") Dispose");
                timer.Dispose();
            }
                
        }

        
    }
}
