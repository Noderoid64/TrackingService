using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using System.Diagnostics;
using MercuryLogger;

namespace Tracker.Net
{
    static class PingController
    {

        public static bool GetPing(string host, int timeOut)
        {
            if(host == null)
            {
                MainLogger.GetInstance().Log("[Fatal] Ping hostname is null");
                return false;
            }
            try
            {
                Ping P = new Ping();
                PingReply Status = P.Send(PingPreProcessing(host), timeOut * 1000);
                if (Status.Status == IPStatus.Success)
                {
                    MainLogger.GetInstance().Log("[Info] Ping to " + host + " Success");
                    return true;
                }
                else
                {
                    MainLogger.GetInstance().Log("[Info] Ping to " + host + " failed (" + Status.Status + ")");
                    return false;
                }
                    
            }
            catch (Exception e)
            {
                MainLogger.GetInstance().Log("[Error] Ping to " + host + " failed\n" + e);
                return false;
            }
            finally
            {
                
            }
            
        }
        private static string PingPreProcessing(string value)
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
