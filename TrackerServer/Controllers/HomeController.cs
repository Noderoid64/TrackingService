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

    public class HomeController : Controller
    {
        ILogger logger;
        static HomeController()
        {
            Sessions = new List<SessionWithKey>();

        }

        public HomeController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger("FileLogger");
        }

        [Route("")]
        public IActionResult login()
        {
            List<string> a = Request.Query.Keys.ToList();
            int ping_error = -1;
            int tracking_value = -1;
            string session_key = Request.Query.FirstOrDefault(p => p.Key == "session_key").Value;
            if (Request.Query.ContainsKey("ping_error"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "ping_error").Value, out ping_error);
            if (Request.Query.ContainsKey("tracking_value"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "tracking_value").Value, out tracking_value);

            if (session_key != null)
            {
                Session s = null;
                if (tracking_value != -1)
                {
                    logger.LogDebugSetTrackingValue(session_key, tracking_value);
                    s = SetData(session_key, tracking_value);
                }
                else if (ping_error != -1)
                {
                    logger.LogDebugSetPingError(session_key, ping_error);
                    s = SetError(session_key, ping_error);
                }
                else
                {
                    logger.LogDebugLogin(session_key);
                    s = SingIn(session_key);

                }
                if (s != null)
                    return Ok(s);
                else
                    return NotFound();
            }
            return NotFound();

        }

        private Session SingIn(string session_key)
        {
            SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
            if (swk != null)
            {
                double a = DateTime.Now.Subtract(swk.lastRequest).TotalSeconds;
                if (a > swk.session_time)
                {
                    Sessions.Remove(swk);
                }
                else 
                return null;
            }
            if (Users.Contains(session_key))
            {
                swk = GetNewSession(session_key);
                Sessions.Add(swk);
                return swk;
            }

            else
                return null;
            // SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
            // if (swk != null && DateTime.Now.Subtract(swk.lastRequest).TotalSeconds > swk.session_time)
            // {
            //     Sessions.Remove(swk);
            //     swk = GetNewSession(session_key);
            //     Sessions.Add(swk);
            //     return (Session)swk;
            // }
            // else
            // {
            //     if (!Users.Contains(session_key))
            //         return null;
            //     else
            //     {
            //         swk = GetNewSession(session_key);
            //         Sessions.Add(swk);
            //         return (Session)swk;
            //     }
            // }




        }
        private Session SetData(string session_key, int tracking_time)
        {
            SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
            if (swk == null)
                return null;
            else
            {
                swk.session_time -= (DateTime.Now.Second - swk.lastRequest.Second) + (DateTime.Now.Minute - swk.lastRequest.Minute) * 60 + (DateTime.Now.Hour - swk.lastRequest.Hour) * 3600;
                if (swk.session_time < 0)
                {
                    swk.session_time = 0;
                    Sessions.Remove(swk);
                }
                swk.lastRequest = DateTime.Now;
                return (Session)swk;
            }
        }
        private Session SetError(string session_key, int ping_error)
        {
            if (!Users.Contains(session_key) || Sessions.FirstOrDefault(p => p.Key == session_key) == null)
                return null;
            else
            {
                SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
                swk.session_time -= (DateTime.Now.Second - swk.lastRequest.Second) + (DateTime.Now.Minute - swk.lastRequest.Minute) * 60 + (DateTime.Now.Hour - swk.lastRequest.Hour) * 3600;
                if (swk.session_time <= 0)
                {
                    swk.session_time = 0;
                    Sessions.Remove(swk);
                }
                swk.lastRequest = DateTime.Now;
                return (Session)swk;
            }
        }

        private SessionWithKey GetNewSession(string session_key)
        {

            SessionWithKey swk = new SessionWithKey();
            swk.Key = session_key;
            swk.session_time = 10;
            swk.tracking_time = 2;
            swk.additional_time = 10;
            swk.lastRequest = DateTime.Now;
            return swk;
        }



        #region data
        List<string> Users = new List<string>()
        {
"qwer",
"asdf",
"zxcv"
        };
        static List<SessionWithKey> Sessions;
        #endregion
    }
}
