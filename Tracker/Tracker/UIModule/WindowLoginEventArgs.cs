using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.UIModule
{
    public class WindowLoginEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public WindowLoginEventArgs(string key)
        {
            Key = key;
        }
    }

}
