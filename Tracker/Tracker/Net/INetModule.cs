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
        ServerResponse SendPingError(string host, ClientRequest request, Action<string> d = null);
        ServerResponse SendValue(string host, ClientRequest request, Action<string> d = null);
        bool SendPing(string hostName);

        
    }
}
