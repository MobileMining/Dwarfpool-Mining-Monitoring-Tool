using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class DwarfpoolMonitor
    {
        private string address;
        private string email;
        private string phone;
        private FrmMain ui;
        private Miner[] miners;

        public DwarfpoolMonitor(FrmMain ui, string address, string email, string phone)
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
                    "No Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ui.stopMonitoringUI();
                return;
            }


            // Testing that the user gave a valid Dwarfpool address to monitor
            this.ui.updateStatus("Validating Dwarfpool wallet address...");

            if (!validateDwarfpoolAddress("http://dwarfpool.com/eth/address/?wallet=" + this.address))
            {

                this.ui.showMessageBox("Given Dwarfpool address does not exist on Dwarfpool servers.",
                    "Invalid Dwarfpool Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ui.stopMonitoringUI();
                return;

            }


            // Get mining statistics
            this.ui.updateStatus("Pulling current mining statistics...");

            double[] statistics = getStatistics("http://dwarfpool.com/eth/address/?wallet=" + this.address);
            this.ui.updateStatistics(statistics[0], statistics[1], statistics[2]);

            this.ui.updateStatus("Getting status of each miner...");

            miners = Miner.getMiners(address);

            if (miners == null)
            {

                DialogResult result = this.ui.showMessageBoxReturnAnswer("There are currently no Dwarfpool miners at the specified wallet" +
                    "address. Would you like to continue to monitor in case one or more miners comes online?", "No Miners Found",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {

                    this.ui.stopMonitoringUI();
                    return;

                }

            }

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
            bool firstRun = true;

            while (true)
            {   

                Thread.Sleep(60000);

                // Get mining statistics
                double[] statistics = getStatistics("http://dwarfpool.com/eth/address/?wallet=" + this.address);
                this.ui.updateStatistics(statistics[0], statistics[1], statistics[2]);

                Miner[] minersOld = miners;

                // Get status of each miner
                miners = Miner.getMiners(address);

                bool allMinersActive = true;

                foreach (Miner miner in miners)
                {

                    if (!miner.isMining())
                    {

                        allMinersActive = false;
                        break;

                    }

                }

                if (!allMinersActive)
                {
                    String minersDown = null;

                    for (int i = 0; i < minersOld.Length; i++)
                    {
                        if (!miners[i].isMining() && (miners[i].isMining() != minersOld[i].isMining() || firstRun))
                        {
                            if (minersDown == null)
                            {

                                minersDown = miners[i].getName();

                            }
                            else
                            {

                                minersDown += "," + miners[i].getName();

                            }
                        }
                    }

                    if (minersDown != null)
                    {

                        sendMinerDownNotifications(minersDown);

                    }

                }

                if (firstRun)
                {

                    firstRun = false;

                }
            }
            
        }

        public void sendMinerDownNotifications(string minersDown)
        {

            int count = 0;

            foreach (char c in minersDown)
            {

                if (c == ',')
                {

                    count++;

                }

            }

            String title;
            String message;

            if (count == 0)
            {

                title = "Miner Down";
                message = "Dwarfpool miner " + minersDown + " is down.";

            }
            else if (count == 1)
            {

                title = "Miners Down";
                message = "Dwarfpool miners " + minersDown.Substring(0, minersDown.IndexOf(',')) +
                    " and " + minersDown.Substring(minersDown.IndexOf(',') + 1) + " are down.";

            }
            else
            {

                title = "Miners Down";
                message = "Dwarfpool miners " + minersDown.Substring(0, minersDown.LastIndexOf(',') + 1) +
                    " and " + minersDown.Substring(minersDown.LastIndexOf(',') + 1) + " are down.";

            }

            this.ui.showMinerDownNotification(title, message);

            if (phone != null)
            {

                SMSSender sms = new SMSSender(phone, "Dwarfpool Mining Monitoring Tool: \n\n" + message);
                sms.send();

            }

            if (email != null)
            {

                EmailSender emailMessage = new EmailSender();
                emailMessage.sendEmail(email, title, message);

            }

        }

        private bool testConnection()
        {

            InternetTools test = new InternetTools();

            return test.testDNS();

        }
    }
}
