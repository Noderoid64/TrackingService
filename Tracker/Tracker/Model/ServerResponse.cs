using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    class ServerResponse
    {
        public int tracking_time { get; set; }
        private int session_time;
        
        public int additional_time { get; set; }
        public DateTime LastResponse { get; private set; }

        public int SessionTime
        {
            get
            {
                return session_time;
            }
            set
            {
                LastResponse = DateTime.Now;
                session_time = value;
            }
        }

        public string show()
        {
            return $"tracking_time: {tracking_time}\n" +
                $"session_time: {session_time}\n" +
                $"additional_time: {additional_time}";
        }
    }
}
