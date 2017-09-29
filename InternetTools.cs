using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class InternetTools
    {

        public bool ping(string address)
        {

            Ping pinger = new Ping();

            try
            {
                PingReply reply = pinger.Send("8.8.8.8");

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (PingException e)
            {
                ;
            }

            return false;

        }
        public bool isConnected()
        {

            return ping("8.8.8.8");

        }

        public bool testDNS()
        {

            if (!isConnected())
            {
                return false;
            }

            return ping("google.com");

        }
        public string getHTMLFromWebPage(string address)
        {
            WebRequest request = WebRequest.Create(address);

            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();

            string html;

            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }

            return html;
        }
    }
}
