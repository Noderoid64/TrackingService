using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions;

namespace TrackerServer.Controllers
{

    public class GateController : Controller
    {
        static SessionWithKey s;
        static GateController()
        {
            s = new SessionWithKey()
            {
                Additional = false,
                tracking_time = 5,
                additional_time = 0,
                session_time = 20,
                lastRequest = DateTime.MinValue,
                Key = "nod"
            };
        }

        [Route("/Gate")]
        public IActionResult login()//ILoggerFactory loggerFactory)
        {
            List<string> a = Request.Query.Keys.ToList();
            int ping_error = -1;
            int tracking_value = -1;
            string session_key = Request.Query.FirstOrDefault(p => p.Key == "session_key").Value;
            if (Request.Query.ContainsKey("ping_error"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "ping_error").Value, out ping_error);
            if (Request.Query.ContainsKey("tracking_value"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "tracking_value").Value, out tracking_value);

            if (s.Key == session_key)
            {
                if (s.Additional) // Если доп время
                {
                    s.additional_time -= GetTotalSecond(s.lastRequest);
                    s.lastRequest = DateTime.Now;
                    if (s.additional_time <= 0)
                    {
                        Restart();
                        return NotFound();
                    }

                    else
                        return Ok(((Session)s));
                }
                else // Если обычное время
                {
                    s.session_time -= GetTotalSecond(s.lastRequest);
                    s.lastRequest = DateTime.Now;
                    if (s.session_time <= 0)
                        //s.Additional = true;
                        return NotFound();
                    Debug.Print(s.Key + "\n" + s.Additional + "/n" + s.additional_time + "/n" + s.session_time + "/n" + s.tracking_time + "/n" + s.lastRequest);
                    return Ok(((Session)s));
                }
            }
            else
                return NotFound();

        }
        private int GetTotalSecond(DateTime time)
        {
            if (time == DateTime.MinValue)
                return 0;
            else
                return (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) - (time.Hour * 3600 + time.Minute * 60 + time.Second);
        }
        private void Restart()
        {
            s = new SessionWithKey()
            {
                Additional = false,
                tracking_time = 3,
                additional_time = 0,
                session_time = 20,
                lastRequest = DateTime.MinValue
            };
        }
    }
}
