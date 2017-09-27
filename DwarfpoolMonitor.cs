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

        }

        private bool testConnection()
        {

            InternetTools test = new InternetTools();

            return test.testDNS();

        }
    }
}
