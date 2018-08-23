using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Tracker.Tools.PostSender
{
    interface IPostSender
    {
        string FormMessage(string host, NameValueCollection properties);
        string Post(string message);
    }
}
