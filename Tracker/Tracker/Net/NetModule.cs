using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

namespace Tracker.Net
{
    public class NetModule : INetModule
    {
        readonly int Timeout;
        public NetModule(int timeout)
        {
            Timeout = timeout;
        }

        public ServerResponse SendPingError(string host, ClientRequest request)
        {
            return PostSender.SendError(host,request.PingError, request.SessionKey);
        }
        public ServerResponse SendValue(string host, ClientRequest request)
        {
            return PostSender.SendValue(host, request.TrackingValue, request.SessionKey);
        }

        public bool SendPing(string hostName)
        {
            return PingController.GetPing(hostName, Timeout);
        }
        
    }
}
