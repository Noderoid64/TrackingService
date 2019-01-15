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
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        static SessionStateKey s;
        static Session session;
        static GateController()
        {
            session = new MainSession(out s);
        }
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Hello");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login()
        {
            SessionParams sessionParams = GetParams(Request);
            if (s.Key == sessionParams.SessionKey)
            {
                if (s.Additional)
                {
                    session = new AdditionalSession();
                }
                else if (s.session_time == 0)
                {
                    session = new MainSession(out s);
                }
                session.Handle(s);
                s.lastRequest = DateTime.Now;
                return Ok(((SessionState)s));
            }
            return NotFound();
        }

        private static SessionParams GetParams(HttpRequest request)
        {
            SessionParams sessionParams;
            sessionParams.PingError = -1;
            sessionParams.TrackingValue = -1;
            sessionParams.SessionKey = request.Query.FirstOrDefault(p => p.Key == "session_key").Value;
            if (request.Query.ContainsKey("ping_error"))
                int.TryParse(request.Query.FirstOrDefault(p => p.Key == "ping_error").Value, out sessionParams.PingError);
            if (request.Query.ContainsKey("tracking_value"))
                int.TryParse(request.Query.FirstOrDefault(p => p.Key == "tracking_value").Value, out sessionParams.TrackingValue);
            return sessionParams;
        }
    }
    internal struct SessionParams
    {
        internal string SessionKey;
        internal int PingError;
        internal int TrackingValue;
    }

    internal abstract class Session
    {
        public abstract void Handle(SessionStateKey session);
        protected static int GetTotalSecond(DateTime time)
        {
            if (time == DateTime.MinValue)
                return 0;
            else
                return (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) - (time.Hour * 3600 + time.Minute * 60 + time.Second);
        }
    }
    internal class MainSession : Session
    {
        public MainSession(out SessionStateKey session)
        {
            session = SessionStateKey.GetNew();
        }
        public override void Handle(SessionStateKey session)
        {
            session.session_time -= GetTotalSecond(session.lastRequest);
            if (session.session_time <= 0)
            {
                session.Additional = true;
                session.session_time = 0;
            }
            return;
        }
    }
    internal class AdditionalSession : Session
    {
        public override void Handle(SessionStateKey session)
        {
            session.additional_time -= GetTotalSecond(session.lastRequest);
            if (session.additional_time <= 0)
            {
                session.additional_time = 0;
                session.Additional = false;
            }
            return;
        }
    }
}
