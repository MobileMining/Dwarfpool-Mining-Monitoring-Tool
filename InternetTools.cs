using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void getHTMLFromWebPage(string address)
        {
            ;
        }
    }
}
