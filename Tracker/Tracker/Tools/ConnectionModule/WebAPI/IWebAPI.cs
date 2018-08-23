using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

namespace Tracker.Tools.ConnectionModule.WebAPI
{
    interface IWebAPI
    {
        ServerResponse LogIn(string session_key);
        ServerResponse SendTrackingTime(string session_key, int tracking_time);
        ServerResponse SendPingError(string session_key, int ping_error);
    }
}
