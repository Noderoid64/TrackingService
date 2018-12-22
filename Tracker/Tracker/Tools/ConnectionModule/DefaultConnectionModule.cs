//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Tracker.Tools.ConnectionModule.WebAPI;
//using Tracker.Tools.PostSender;
//using Tracker.Model;

//namespace Tracker.Tools.ConnectionModule
//{
//    class DefaultConnectionModule : ConnectionModule
//    {
//        IWebAPI webAPI;
//        public DefaultConnectionModule()
//        {
//            webAPI = new DefaultWebAPI(new DefaultPostSender());
//        }
//        public override ServerResponse StartAdditionalSession(string key)
//        {
//            throw new NotImplementedException();
//        }

//        public override ServerResponse StartSession(string key)
//        {
//            return webAPI.LogIn(key);
//        }
//    }
//}
