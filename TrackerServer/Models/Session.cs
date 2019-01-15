using System;
namespace TrackerServer
{
    public class SessionState
    {
        public bool Additional { get; set; }
        public int tracking_time { get; set; }
        public int session_time { get; set; }
        public int additional_time { get; set; }
    }

    public class SessionStateKey : SessionState
    {
        public string Key { get; set; }
        public DateTime lastRequest { get; set; }
        public static SessionStateKey GetNew()
        {
            return new SessionStateKey()
            {
                Additional = false,
                tracking_time = 2,
                additional_time = 0,
                session_time = 5,
                lastRequest = DateTime.MinValue,
                Key = "nod"
            };
        }
    }
}