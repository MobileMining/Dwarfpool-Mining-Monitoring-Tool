using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class DwarfpoolMonitor
    {
        private string address;
        private string email;
        private int phone;
        private FrmMain ui;

        public DwarfpoolMonitor(FrmMain ui, string address, string email, int phone)
        {

            this.ui = ui;
            this.address = address;
            this.email = email;
            this.phone = phone;

        }

        public void start()
        {

            this.ui.updateStatus("Testing for an Internet connection...");

            if (!testConnection())
            {
                this.ui.showMessageBox("Failed to connect to the Internet. Please check your Internet connection.",
                    "No Internet Connection", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            this.ui.updateStatus("Validating Dwarfpool wallet address...");

            InternetTools validate = new InternetTools();

            string html = validate.getHTMLFromWebPage("http://dwarfpool.com/eth/address/?wallet=adf6a3cFe5447cF195676B911DeFd68Aa8cf44Eb");

            int cutIndex;

            // Get current balance


            html = html.Substring(html.IndexOf(" ETH") - 10);

            double currentBalance = Double.Parse(html.Substring(0, 10));
            

            // Get last earnings 24 hours
            html = html.Substring(html.IndexOf(" ETH<br><span style=\"font-size:85%;\">") - 10);

            double earningsLast24 = Double.Parse(html.Substring(0, 10));

            // Get current price
            // May change this later to get the price from a different site

            html = html.Substring(html.IndexOf("Rates"));
            html = html.Substring(html.IndexOf("</span>") - 10);

            double currentPrice = Double.Parse(html.Substring(0, 8));


        }

        private bool testConnection()
        {

            InternetTools test = new InternetTools();

            return test.testDNS();

        }
    }
}
