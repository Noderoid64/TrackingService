using System;
namespace TrackerServer
{
    public class Session
    {
        public int tracking_time { get; set; }
        public double session_time { get; set; }
        public int additional_time { get; set; }
    }

    public class SessionDto
    {
        public int tracking_time { get; set; }
        public int session_time { get; set; }
        public int additional_time { get; set; }

        public SessionDto(Session s)
        {
            tracking_time = s.tracking_time;
            session_time = (int) s.session_time;
            additional_time = s.additional_time;
        }
    }

    public class SessionWithKey : Session
    {
        public string Key { get; set; }
        public DateTime lastRequest { get; set; }


    }
}