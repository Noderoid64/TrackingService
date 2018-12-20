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

        public static bool GetPing(string host, int timeOut)
        {
            AppLoger.Log("Send ping to " + PreProcessing(host));
            try
            {
                Ping P = new Ping();
                PingReply Status = P.Send(PreProcessing(host), timeOut * 1000);
                AppLoger.Log("Receive ping" + Status.Status);
                if (Status.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            catch
            {

                Debug.Print("PingError");
                return false;
            }
            finally
            {
                
            }
            
        }
        private static string PreProcessing(string value)
        {
            string b = value.ToLower();
            if (b.StartsWith("http://"))
                value = value.Remove(0, 7);
            else if (b.StartsWith("https://"))
                value = value.Remove(0,8);
            return value.Split('/')[0];
            
        }
    }
}
