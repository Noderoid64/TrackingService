using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker.Model;

using MercuryLogger;

namespace Tracker.Net
{
    public class NetModule : INetModule
    {
        readonly int Timeout;
        public NetModule(int timeout)
        {
            Timeout = timeout;
        }

        public ServerResponse SendPingError(string host, ClientRequest request, Action<string> d = null)
        {
            MainLogger.GetInstance().Log("[Info] send Ping Error to " + host + " SessionKey: " + request.SessionKey + " ping error: " + request.PingError);
            return PostSender.SendError(host,request.PingError, request.SessionKey);
        }
        public ServerResponse SendValue(string host, ClientRequest request, Action<string> d = null)
        {
            MainLogger.GetInstance().Log("[Info] send TrackingValue to " + host + " SessionKey: " + request.SessionKey  + " TrackingValue: " + request.TrackingValue);
            ServerResponse sr;
            try
            {
                sr = PostSender.SendValue(host, request.TrackingValue, request.SessionKey);
                MainLogger.GetInstance().Log("[Info] Get response " + " SessionTime: " + sr.session_time + " TrackingTime: " + sr.tracking_time + " AdditionalTime: " + sr.additional_time);
                return sr;
            }
            catch (Exception e)
            {
                if (d != null)
                    d.Invoke(e.Message);
                return null;
            }

            
        }

        public bool SendPing(string hostName)
        {
            return PingController.GetPing(hostName, Timeout);
        }
    }
}
