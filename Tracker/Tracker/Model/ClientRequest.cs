using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class ClientRequest
    {
        public int TrackingValue { get; set; }
        public int PingError { get; set; }
        public string SessionKey { get; set; }
    }
}
