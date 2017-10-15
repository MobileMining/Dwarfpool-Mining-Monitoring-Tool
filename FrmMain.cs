using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        // monitorThread will run all the monitoring activities of the program separate from the UI
        private Thread monitorThread;

        private void frmMain_Load(object sender, EventArgs e)
        {

            // Only doing this for debugging purposes; will implement proper cross-thread calls later
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            // Don't proceed if there is no address in the wallet address box
            if (txtWalletAddress.Text == "")
            {

                showMessageBox("Please enter an Ethereum wallet address.", "No Ethereum Wallet Address", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                // First, set UI to monitoring UI, then perform all other actions
                startMonitoringUI();

                // Will store the given phone number and email address if provided
                string phoneNumber = null;
                string email = null;

                // Get email address from UI
                if (chkEmail.Checked)
                {

                    email = txtEmailAddress.Text;

                }

                // Get phone number from UI
                if (chkText.Checked)
                {

                    phoneNumber = txtPhoneNumber.Text;

                }

                // Create new instance of the monitoring class
                DwarfpoolMonitor monitor = new DwarfpoolMonitor(this, txtWalletAddress.Text, email, phoneNumber);

                // Put this instance in a new thread to avoid UI lockup
                monitorThread = new Thread(new ThreadStart(monitor.start));

                // Start monitoring process
                monitorThread.Start();

            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            // This is unsafe... we're going to have to find a better way to stop the monitoring thread
            monitorThread.Abort();

            // After monitoring has stopped, change UI back to original
            stopMonitoringUI();

        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {

            // Enable and disable phone number box with checkbox
            if (chkText.Checked)
            {

                lblPhoneNumber.Enabled = true;
                txtPhoneNumber.Enabled = true;

            }
            else if (!chkText.Checked)
            {

                lblPhoneNumber.Enabled = false;
                txtPhoneNumber.Enabled = false;

            }

        }

        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {

            // Enable and disable email box with checkbox
            if (chkEmail.Checked)
            {

                lblEmailAddress.Enabled = true;
                txtEmailAddress.Enabled = true;

            }
            else if (!chkEmail.Checked)
            {

                lblEmailAddress.Enabled = false;
                txtEmailAddress.Enabled = false;

            }

        }

        public void startMonitoringUI()
        {

            // When monitoring starts, reflect this in the UI
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            grpStatistics.Enabled = true;
            lblWalletAddress.Enabled = false;
            lbl0x.Enabled = false;
            txtWalletAddress.Enabled = false;
            chkText.Enabled = false;
            chkEmail.Enabled = false;
            lblPhoneNumber.Enabled = false;
            lblEmailAddress.Enabled = false;
            txtPhoneNumber.Enabled = false;
            txtEmailAddress.Enabled = false;

        }

        public void stopMonitoringUI()
        {

            // When monitoring stops, reflect this in the UI
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            grpStatistics.Enabled = false;
            lblWalletAddress.Enabled = true;
            lbl0x.Enabled = true;
            txtWalletAddress.Enabled = true;
            chkText.Enabled = true;
            chkEmail.Enabled = true;

            if (chkText.Checked)
            {

                lblPhoneNumber.Enabled = true;
                txtPhoneNumber.Enabled = true;

            }

            if (chkEmail.Checked)
            {

                lblEmailAddress.Enabled = true;
                txtEmailAddress.Enabled = true;
            }

            // Reset labels and change status
            lblPrice.Text = "Current Price:";
            lblBalance.Text = "Current Dwarfpool Balance:";
            lblEarnings.Text = "Earnings in the last 24 hours:";
            updateStatus("Stopped");

        }

        /* 
         * Updates the statistics currently displayed in the statistics
         * box on the UI.
         */
        public void updateStatistics(double price, double balance, double earnings24Hours)
        {

            // Update all three labels
            lblPrice.Text = "Current Price: $" + String.Format("{0:F2}", price) + " USD";
            lblBalance.Text = "Current Dwarfpool Balance: " + balance + " ETH";
            lblEarnings.Text = "Earnings in the last 24 hours: " + earnings24Hours + " ETH";

        }

        /*
         * Updates the text shown in the status bar at the bottom of the UI.
         */
        public void updateStatus(string status)
        {

            lblStatus.Text = "Status: " + status;

        }

        /*
         * Shows a message box with given parameters. This method is able to
         * be called from the monitoring thread and allows for the monitoring
         * thread to display message boxes on the UI thread.
         */
        public void showMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {

            MessageBox.Show(message, title, buttons, icon);

        }

        /*
         * Same as above, but also returns the answer if the user is given a choice
         * between several buttons.
         */
        public DialogResult showMessageBoxReturnAnswer(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {

            DialogResult result = MessageBox.Show(message, title, buttons, icon);

            return result;

        }

        /*
         * Shows a balloon tip in the taskbar with the given title and message.
         * This method can be called from the monitoring thread and allows for
         * the monitoring thread to display balloon tips on the UI thread.
         */
        public void showMinerDownNotification(string title, string message)
        {

            sysTrayIcon.ShowBalloonTip(10000, title, message, ToolTipIcon.Warning);

        }

        /*
         * Allows for only numbers to be entered in the phone number text box.
         */
        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
