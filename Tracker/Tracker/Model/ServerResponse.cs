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
        public int session_time { get; set; }        
        public int additional_time { get; set; }



        public string show()
        {
            return $"tracking_time: {tracking_time}\n" +
                $"session_time: {session_time}\n" +
                $"additional_time: {additional_time}";
        }
    }
}
