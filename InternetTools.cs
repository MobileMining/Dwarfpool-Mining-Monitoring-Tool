/**
 * -----Dwarfpool Mining Monitoring Tool-----
 * Class: InternetTools
 * Description: A class that contains methods that
 * can be used to perform tasks such as pinging an 
 * address, testing an Internet connection, and 
 * getting HTML code from a given webpage.
 * 
 * Author: David Goguen
 * Email: david.goguen@outlook.com
 */

using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class InternetTools
    {

        /*
         * Pings an IP address or hostname and returns
         * true if the ping was successful. Returns
         * false otherwise.
         */
        public bool ping(string address)
        {

            // Use ping object to perform ping
            Ping pinger = new Ping();

            try
            {

                // Try pinging the address and get the reply
                PingReply reply = pinger.Send(address);

                // Only return true if ping was successful
                if (reply.Status == IPStatus.Success)
                {

                    return true;

                }

            }
            catch (PingException e) // No need to handle this exception here for now
            {

                ;

            }

            // Return false if ping was unsuccessful
            return false;

        }

        /*
         * Tests the internet connection by pinging
         * Google DNS server. Returns true if this ping
         * was successful and false otherwise.
         */
        public bool isConnected()
        {

            return ping("8.8.8.8");

        }

        /*
         * Tests to make sure that DNS is working properly
         * by trying to ping google.com. This ping only
         * occurs if isConnected() runs successfully first.
         * Returns true if this ping was successful and 
         * false otherwise.
         */
        public bool testDNS()
        {

            // Only proceed with testing DNS if ping to an IP address was successful first
            if (!isConnected())
            {

                return false;

            }

            return ping("google.com");

        }

        /*
         * Gets the HTML code from the web address passed
         * to this method and returns it in string format.
         */
        public string getHTMLFromWebPage(string address)
        {

            // Store website information in request
            WebRequest request = WebRequest.Create(address);

            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();

            string html;

            // Convert HTML information to string
            using (StreamReader sr = new StreamReader(data))
            {

                html = sr.ReadToEnd();

            }

            return html;

        }
    }
}
