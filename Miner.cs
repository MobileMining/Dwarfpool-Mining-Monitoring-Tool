/**
 * -----Dwarfpool Mining Monitoring Tool-----
 * Class: Miner
 * Description: A class used to store information 
 * about each miner on a given Dwarfpool address.
 * 
 * Author: David Goguen
 * Email: david.goguen@outlook.com
 */

using System;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    class Miner
    {

        // Stores the miner's number from Dwarfpool
        private int number;

        // Stores the miner's name from Dwarfpool
        private string name;

        // Flag to store the status of the miner
        private bool isActive;

        public Miner(int number, string name, bool isActive)
        {

            this.number = number;
            this.name = name;
            this.isActive = isActive;

        }

        /*
         * Gets all the current miners from a Dwarfpool address
         * and returns an array of Miner objects containing
         * their information.
         */
        public static Miner[] getMiners(string address)
        {

            // Get HTML from Dwarfpool page
            InternetTools tools = new InternetTools();
            string html = tools.getHTMLFromWebPage("http://dwarfpool.com/eth/address/?wallet=" + address);

            // Find the spot in the HTML code where the miner information is located
            html = html.Substring(html.IndexOf("Worker") + 1);
            html = html.Substring(html.IndexOf("Worker"));
            html = html.Substring(html.IndexOf("<tr><td>") + 8, html.IndexOf("</div>"));

            // Stores number of miners found on Dwarfpool
            int count = 0;

            // See if there are any miners on the page
            for (int i = 1; true; i++, count++)
            {

                try
                {

                    html.Substring(html.IndexOf(i + ". "));

                }
                catch (ArgumentOutOfRangeException) // Break loop once all miners have been counted
                {

                    break;

                }

            }

            // Don't bother proceeding if no miners have been found, just return null
            if (count == 0)
            {

                return null;

            }

            // Store each miner in this array
            Miner[] miners = new Miner[count];

            // Get actual miner statistics
            for (int i = 0; i < count; i++)
            {

                string nameCheck;
                bool isActiveCheck = true;

                nameCheck = html.Substring(3, html.IndexOf("</td>") - 3);

                html = html.Substring(html.IndexOf("\">") + 2);

                // If miner displays 'calc', this means it is down
                if (html.Substring(0, 4) == "calc")
                {

                    isActiveCheck = false;

                }

                // Initialize new miner in array
                miners[i] = new Miner(i + 1, nameCheck, isActiveCheck);

                // Don't try cutting string again if we are on the last miner; doing so will throw an exception
                if (i != count - 1)
                {
                    html = html.Substring(html.IndexOf((i + 2) + ". "));
                }
                
            }

            return miners;

        }

        /*
         * Returns the current status of the Miner.
         */
        public bool isMining()
        {

            return this.isActive;

        }

        /*
         * Returns the name of the Miner.
         */
        public string getName()
        {

            return this.name;

        }

        /*
         * Returns the Miner's number from Dwarfpool.
         */
        public int getNumber()
        {

            return this.number;

        }
    }
}
