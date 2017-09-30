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

        private Thread monitorThread;

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.btnStop.Enabled = false;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {   
            if (txtWalletAddress.Text == "")
            {
                showMessageBox("Please enter an Ethereum wallet address.", "No Ethereum Wallet Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                startMonitoringUI();

                DwarfpoolMonitor monitor = new DwarfpoolMonitor(this, txtWalletAddress.Text, "test", 949);

                monitorThread = new Thread(new ThreadStart(monitor.start));

                monitorThread.Start();

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            monitorThread.Abort();
            stopMonitoringUI();
        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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

            btnStop.Enabled = true;
            btnStart.Enabled = false;
            grpStatistics.Enabled = true;
            lblWalletAddress.Enabled = false;
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

            btnStop.Enabled = false;
            btnStart.Enabled = true;
            grpStatistics.Enabled = false;
            lblWalletAddress.Enabled = true;
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

            lblPrice.Text = "Current Price:";
            lblBalance.Text = "Current Dwarfpool Balance:";
            lblEarnings.Text = "Earnings in the last 24 hours:";

            updateStatus("Stopped");

        }

        public void updateStatistics(double price, double balance, double earnings24Hours)
        {
            lblPrice.Text = "Current Price: $" + price + " USD";
            lblBalance.Text = "Current Dwarfpool Balance: " + balance + " ETH";
            lblEarnings.Text = "Earnings in the last 24 hours: " + earnings24Hours + " ETH";
        }

        public void updateStatus(string status)
        {
            lblStatus.Text = "Status: " + status;
        }

        public void showMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {

            MessageBox.Show(message, title, buttons, icon);

        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
