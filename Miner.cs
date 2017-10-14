using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    class Miner
    {
        private int number;
        private string name;
        private bool isActive;

        public Miner(int number, string name, bool isActive)
        {

            this.number = number;
            this.name = name;
            this.isActive = isActive;

        }

        public static Miner[] getMiners(string address)
        {

            InternetTools tools = new InternetTools();
            string html = tools.getHTMLFromWebPage("http://dwarfpool.com/eth/address/?wallet=" + address);

            html = html.Substring(html.IndexOf("Worker") + 1);
            html = html.Substring(html.IndexOf("Worker"));
            html = html.Substring(html.IndexOf("<tr><td>") + 8, html.IndexOf("</div>"));

            //count number of miners
            int count = 0;

            for (int i = 1; true; i++, count++)
            {
                try
                {

                    html.Substring(html.IndexOf(i + ". "));

                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            if (count == 0)
            {
                return null;
            }

            Miner[] miners = new Miner[count];

            for (int i = 0; i < count; i++)
            {

                string nameCheck;
                bool isActiveCheck = true;

                nameCheck = html.Substring(3, html.IndexOf("</td>") - 3);

                html = html.Substring(html.IndexOf("\">") + 2);

                if (html.Substring(0, 4) == "calc")
                {

                    isActiveCheck = false;

                }

                miners[i] = new Miner(i + 1, nameCheck, isActiveCheck);

                if (i != count - 1)
                {
                    html = html.Substring(html.IndexOf((i + 2) + ". "));
                }
                
            }

            return miners;

        }

        public bool isMining()
        {
            return this.isActive;
        }

        public string getName()
        {
            return this.name;
        }

        public int getNumber()
        {
            return this.number;
        }
    }
}
