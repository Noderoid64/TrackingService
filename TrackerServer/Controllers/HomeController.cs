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
        public IActionResult login()//ILoggerFactory loggerFactory)
        {
            List<string> a = Request.Query.Keys.ToList();
            int ping_error = -1;
            int tracking_value = -1;
            string session_key = Request.Query.FirstOrDefault(p => p.Key == "session_key").Value;
            if (Request.Query.ContainsKey("ping_error"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "ping_error").Value, out  ping_error);
            if (Request.Query.ContainsKey("tracking_value"))
                int.TryParse(Request.Query.FirstOrDefault(p => p.Key == "tracking_value").Value, out  tracking_value);

            // var logger = loggerFactory.CreateLogger("FileLogger");

            // logger.LogDebug("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "]" + "Take request " + "session_Key: " + session_key + " tracking_time:" + tracking_time + " ping_error:" + ping_error);
            if (session_key != null)
            {
               // session_key = session_key.Remove(0, 1);
              //  session_key = session_key.Remove(session_key.Length - 1, 1);
                Session s = null;
                if (tracking_value != -1)
                {
                    logger.LogDebug("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "]" + "SetData " + "session_Key: " + session_key + " tracking_value:" + tracking_value);
                    // tracking_time = tracking_time.Remove(0, 1);
                    // tracking_time = tracking_time.Remove(tracking_time.Length - 1, 1);
                    s = SetData(session_key, tracking_value);
                }
                else if (ping_error != -1)
                {
                    logger.LogDebug("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "]" + "SetError " + "session_Key: " + session_key + " ping_error:" + ping_error);
                    //ping_error = ping_error.Remove(0, 1);
                    // ping_error = ping_error.Remove(ping_error.Length - 1, 1);
                    s = SetError(session_key, ping_error);
                }
                else
                {
                    logger.LogDebug("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "]" + "SetData " + "session_Key: " + session_key );
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
            if (!Users.Contains(session_key) || Sessions.FirstOrDefault(p => p.Key == session_key) != null)
                return null;
            else
            {
                SessionWithKey swk = GetNewSession(session_key);
                Sessions.Add(swk);
                return (Session)swk;
            }
        }
        private Session SetData(string session_key, int tracking_time)
        {
            if (!Users.Contains(session_key) || Sessions.FirstOrDefault(p => p.Key == session_key) == null)
                return null;
            else
            {
                SessionWithKey swk = Sessions.FirstOrDefault(p => p.Key == session_key);
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
