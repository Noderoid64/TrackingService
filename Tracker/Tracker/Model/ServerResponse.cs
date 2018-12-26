using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class ServerResponse
    {
        public int tracking_time { get; set; }
        public int session_time { get; set; }
        public int additional_time { get; set; }

    }
}
