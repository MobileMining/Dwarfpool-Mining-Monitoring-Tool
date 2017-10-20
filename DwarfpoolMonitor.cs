/**
 * -----Dwarfpool Mining Monitoring Tool-----
 * Class: DwarfpoolMonitor
 * Description: A class used to monitor miners at
 * a given Dwarfpool address. Contains an infinite 
 * monitoring loop, so invoking the start() method 
 * should always be done on a separate thread.
 * 
 * Author: David Goguen
 * Email: david.goguen@outlook.com
 */

using System;
using System.Threading;
using System.Windows.Forms;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class DwarfpoolMonitor
    {

        // Stores Ethereum wallet address we are monitoring on Dwarfpool
        private string address;

        // Stores email address we are sending notifications to, if there is one
        private string email;

        // Stores phone number we are sending notifications to, if there is one
        private string phone;

        // Store a reference to the UI so we can make changes on it
        private FrmMain ui;

        // Store an array of the current miners
        private Miner[] miners;

        public DwarfpoolMonitor(FrmMain ui, string address, string email, string phone)
        {

            this.ui = ui;
            this.address = address;
            this.email = email;
            this.phone = phone;

        }

        /*
         * Starts the actual monitoring thread by testing needed conditions
         * before starting the monitor loop.
         */
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

        /*
         * Validates that the given address actually is currently
         * being used to mine on Dwarfpool. Returns true if it
         * is and false otherwise.
         */
        public bool validateDwarfpoolAddress(string address)
        {

            InternetTools validate = new InternetTools();

            string html = validate.getHTMLFromWebPage(address);

            // Return false if no HTML from webpage or if we get an invalid wallet error
            if (html == " " || html == "invalid wallet")
            {
                return false;
            }

            // Return true if the web page does exist
            return true;

        }

        /*
         * Checks the current mining statistics at the given Ethereum address
         * and returns an array of doubles containing this information. The
         * first number in the array contains the current price for 1 Ether
         * in USD, the second contains the current Dwarfpool balance at the
         * address and the third contains the earnings of the Dwarfpool
         * address in the last 24 hours.
         */
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

        /*
         * Contains a loop that monitors the Dwarfpool address
         * until the thread is killed. Loop runs once a minute.
         */
        private void monitor()
        {

            bool firstRun = true;

            while (true)
            {   

                // Only run once every minute
                Thread.Sleep(60000);

                // Get mining statistics
                double[] statistics = getStatistics("http://dwarfpool.com/eth/address/?wallet=" + this.address);
                this.ui.updateStatistics(statistics[0], statistics[1], statistics[2]);

                Miner[] minersOld = miners;

                // Get status of each miner
                miners = Miner.getMiners(address);

                bool allMinersActive = true;

                // Test to see if any miners have gone down
                foreach (Miner miner in miners)
                {

                    if (!miner.isMining())
                    {

                        allMinersActive = false;
                        break;

                    }

                }

                // If a miner has gone down, see which ones and get their names
                if (!allMinersActive)
                {
                    String minersDown = null;

                    for (int i = 0; i < minersOld.Length; i++)
                    {

                        if (!miners[i].isMining() && (miners[i].isMining() != minersOld[i].isMining() || firstRun))
                        {

                            // Formatting the string since this is what we are going to use to compose our message later
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

                    // Send notifications if there are miners down
                    if (minersDown != null)
                    {

                        sendMinerDownNotifications(minersDown);

                    }

                }

                // Change firstRun to false after the loop has run once
                if (firstRun)
                {

                    firstRun = false;

                }

            }
            
        }

        /*
         * Sends notifications to the email address and/or cell phone
         * number specified. Also shows a balloon tip on the PC this
         * application is running on. Takes a string saying which miners
         * are down as a parameter.
         */
        public void sendMinerDownNotifications(string minersDown)
        {

            // Count number of miners we are being given (n - 1)
            int count = 0;

            // Add 1 to count each time a comma is found
            foreach (char c in minersDown)
            {

                if (c == ',')
                {

                    count++;

                }

            }

            // Store the title and message we are going to send
            String title;
            String message;

            // Formatting purposes; compose the message and title
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

            // Show balloon tip on PC
            this.ui.showMinerDownNotification(title, message);

            // If a phone number has been given, send a text message
            if (phone != null)
            {

                SMSSender sms = new SMSSender(phone, "Dwarfpool Mining Monitoring Tool: \n\n" + message);
                sms.send();

            }

            // If an email has been given, send an email
            if (email != null)
            {

                EmailSender emailMessage = new EmailSender();
                emailMessage.sendEmail(email, title, message);

            }

        }

        /*
         * Tests that there is a full internet connection by testing
         * DNS.
         */
        private bool testConnection()
        {

            InternetTools test = new InternetTools();

            return test.testDNS();

        }
    }
}
