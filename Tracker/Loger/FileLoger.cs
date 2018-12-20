using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loger
{
    public class FileLoger : Loger
    {
        Object obj = new Object();
        string path;

        public FileLoger(string name = "Log")
        {
            if (name == "Log")
                name += DateTime.Now.Hour + "." + DateTime.Now.Minute +"."+ DateTime.Now.Second;
            path = name + ".txt";
        }



        public override void Log(string message, MessageType type = MessageType.Debug)
        {
            lock (obj)
            {
                if (!Directory.Exists("Logs")) Directory.CreateDirectory("Logs");
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter("Logs/" + path, true))
                {
                    writer.WriteLine("[" + type.ToString() + "]" + "[" + String.Format("{0,2}:{1,2}:{2,2}:{3,2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond) + "] " + message );
                }
            }
        }
        
    }
}
