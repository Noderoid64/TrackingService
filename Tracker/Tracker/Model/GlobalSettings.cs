using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    internal class GlobalSettings
    {
        #region Singleton
        private static GlobalSettings instance;
        private static object syncRoot = new Object();

        private GlobalSettings() {
            ClientRequest = new ClientRequest();
        }

        public static GlobalSettings GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new GlobalSettings();
                }
            }
            return instance;
        }
        #endregion
        public string ServerUrl { get; set; }
        public string PingUrl { get; set; }
        public int PingTimeout { get; set; }
        public bool LoggerActive { get; set; }

        public bool IsAdditional { get; set; }

        public ServerResponse ServerResponse { get; set; }
        public ClientRequest ClientRequest { get; set; }
    }
}
