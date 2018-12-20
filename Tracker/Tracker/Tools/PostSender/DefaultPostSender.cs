using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;

namespace Tracker.Tools.PostSender
{
    internal class DefaultPostSender : IPostSender
    {


        public string FormMessage(string host, NameValueCollection properties)
        {
            string str = "?";
            for (int i = 0; i < properties.Count; i++)
            {
                str += properties.GetKey(i) + "=" + properties.Get(i);
                if (i != properties.Count - 1)
                    str += "&";
            }
            return (PreProcessing(host) + str.ToString());
        }

        public string Post(string message)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(message);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes("");
            req.ContentLength = sentData.Length;
            System.IO.Stream sendStream;
            try
            {
                sendStream = req.GetRequestStream();
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
            }
            catch (WebException)
            {
                return "-2";
            }


            System.Net.WebResponse res;
            try
            {
                res = req.GetResponse();
            }
            catch (WebException we)
            {
                return "-3";
            }

            System.IO.Stream ReceiveStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
        private string PreProcessing(string value)
        {
            if (!value.ToLower().StartsWith("http://"))
                return "http://" + value;
            else
                return value;
        }
    }
}
