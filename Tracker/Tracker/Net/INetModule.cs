using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

namespace Tracker.Net
{
    interface INetModule
    {
        ServerResponse SendPingError(string host, ClientRequest request);
        ServerResponse SendValue(string host, ClientRequest request);
        bool SendPing(string hostName);
    }
}
