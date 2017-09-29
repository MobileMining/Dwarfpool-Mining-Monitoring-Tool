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

            // Testing for an Internet connection
            this.ui.updateStatus("Testing for an Internet connection...");

            if (!testConnection())
            {
                this.ui.showMessageBox("Failed to connect to the Internet. Please check your Internet connection.",
                    "No Internet Connection", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }


            // Testing that the user gave a valid Dwarfpool address to monitor
            this.ui.updateStatus("Validating Dwarfpool wallet address...");

            if (!validateDwarfpoolAddress("http://dwarfpool.com/eth/address/?wallet=" + this.address))
            {

                this.ui.showMessageBox("Given Dwarfpool address does not exist on Dwarfpool servers.",
                    "Invalid Dwarfpool Address", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;

            }


            // Get mining statistics
            this.ui.updateStatus("Pulling current mining statistics...");

            double[] statistics = getStatistics("http://dwarfpool.com/eth/address/?wallet=" + this.address);
            this.ui.updateStatistics(statistics[0], statistics[1], statistics[2]);

            // Start monitoring loop
            this.ui.updateStatus("Monitoring");
            monitor();
        }

        public bool validateDwarfpoolAddress(string address)
        {

            InternetTools validate = new InternetTools();

            string html = validate.getHTMLFromWebPage(address);

            if (html == " " || html == "invalid wallet")
            {
                return false;
            }

            return true;

        }

        public double[] getStatistics(string address)
        {

            InternetTools webpage = new InternetTools();
            string html = webpage.getHTMLFromWebPage(address);

            // Get current balance
            html = html.Substring(html.IndexOf(" ETH") - 10);
            double currentBalance = Double.Parse(html.Substring(0, 10));

            // Get last earnings 24 hours
            html = html.Substring(html.IndexOf(" ETH<br><span style=\"font-size:85%;\">") - 10);
            double earningsLast24 = Double.Parse(html.Substring(0, 10));

            // Get current price
            html = webpage.getHTMLFromWebPage("https://ethereumprice.org");
            html = html.Substring(html.IndexOf("\"rp\">") + 5);
            html = html.Substring(0, html.IndexOf("</span>"));
            double currentPrice = Double.Parse(html);

            // Store all stats in an array of doubles
            double[] statistics = new double[3];

            statistics[0] = currentPrice;
            statistics[1] = currentBalance;
            statistics[2] = earningsLast24;

            return statistics;

        }

        private void monitor()
        {
            ;
        }

        private bool testConnection()
        {

            InternetTools test = new InternetTools();

            return test.testDNS();

        }
    }
}
