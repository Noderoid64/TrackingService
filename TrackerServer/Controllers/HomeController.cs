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
                    s = SetData(session_key, tracking_value);
                }
                else if (ping_error != -1)
                {
                    s = SetError(session_key, ping_error);
                }
                else
                {
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
                {
                    swk.lastRequest = DateTime.Now;
                    return null;
                }

            }
            if (Users.Contains(session_key))
            {
                swk = GetNewSession(session_key);
                Sessions.Add(swk);
                logger.LogDebugLogin(session_key);
                return swk;
            }

            else
                return null;
        }
        private Session SetData(string session_key, int tracking_value)
        {
            SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
            if (swk != null)
            {
                double a = DateTime.Now.Subtract(swk.lastRequest).TotalSeconds;
                if (a > swk.session_time)
                {
                    Sessions.Remove(swk);
                    return null;
                }
                else
                {
                    swk.lastRequest = DateTime.Now;
                    swk.session_time -= (int)a;
                    logger.LogDebugSetTrackingValue(session_key, tracking_value);
                    return swk;
                }
            }
            else
                return null;
        }
        private Session SetError(string session_key, int ping_error)
        {
            SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
            if (swk != null)
            {
                double a = DateTime.Now.Subtract(swk.lastRequest).TotalSeconds;
                if (a > swk.session_time)
                {
                    Sessions.Remove(swk);
                    return null;
                }
                else
                {
                    swk.lastRequest = DateTime.Now;
                    swk.session_time -= (int)a;
                    logger.LogDebugSetPingError(session_key, ping_error);
                    return swk;
                }
            }
            else
                return null;
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
