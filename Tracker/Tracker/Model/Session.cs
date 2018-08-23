using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    class Session
    {
        //private static Session session;
        //public static Session GetInstance()
        //{
        //    if (session == null)
        //        session = new Session();
        //    return session;
        //}
        //private Session() { }
        public int TrackingTime { get; set;}
        public string SessionKey { get; set;}
        public int PingError { get; set; }

        
    }
}
