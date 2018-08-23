using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using System.Diagnostics;

namespace Tracker.Tools.PingModule
{
    static class PingController
    {

        public static bool GetPing(string host)
        {
            try
            {
                Ping P = new Ping();
                PingReply Status = P.Send(host, 1);
                if (Status.Status == IPStatus.Success)
                {
                    Debug.Print("Ping true : " + Status.Status);
                    return true;
                }
                else
                {
                    Debug.Print("Ping false :" + Status.Status);
                    return false;
                }
                    
            }
            catch
            {

                Debug.Print("PingError");
                return false;
            }

        }
    }
}
