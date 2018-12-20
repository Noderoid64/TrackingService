using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loger
{
   public abstract class Loger
    {
        public Loger next { set; protected get; }
        public abstract void Log(string message, MessageType type = MessageType.Debug);
    }
}
