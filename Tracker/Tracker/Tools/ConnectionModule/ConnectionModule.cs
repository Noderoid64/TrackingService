using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.Tools.ConnectionModule
{
    abstract class ConnectionModule
    {        
        public abstract ServerResponse StartSession(string key);
        public abstract ServerResponse StartAdditionalSession(string key);
    }
}
