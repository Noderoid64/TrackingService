using System;
namespace TrackerServer
{
    public class Session
    {
        public int tracking_time { get; set; }
        public int session_time { get; set; }
        public int additional_time { get; set; }
    }

    public class SessionWithKey : Session
    {
        public string Key { get; set; }
        public DateTime lastRequest{get;set;}

       
    }
}