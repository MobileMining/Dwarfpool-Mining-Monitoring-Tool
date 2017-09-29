namespace Dwarfpool_Mining_Monitoring_Tool
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.stsStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblWalletAddress = new System.Windows.Forms.Label();
            this.txtWalletAddress = new System.Windows.Forms.TextBox();
            this.chkText = new System.Windows.Forms.CheckBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblEarnings = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.stsStrip.SuspendLayout();
            this.grpStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsStrip
            // 
            this.stsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.stsStrip.Location = new System.Drawing.Point(0, 278);
            this.stsStrip.Name = "stsStrip";
            this.stsStrip.Size = new System.Drawing.Size(372, 22);
            this.stsStrip.SizingGrip = false;
            this.stsStrip.TabIndex = 0;
            this.stsStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 17);
            this.lblStatus.Text = "Status: Monitoring stopped";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(100, 250);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(127, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Monitoring";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(233, 250);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(127, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop Monitoring";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblWalletAddress
            // 
            this.lblWalletAddress.AutoSize = true;
            this.lblWalletAddress.Location = new System.Drawing.Point(9, 15);
            this.lblWalletAddress.Name = "lblWalletAddress";
            this.lblWalletAddress.Size = new System.Drawing.Size(129, 13);
            this.lblWalletAddress.TabIndex = 3;
            this.lblWalletAddress.Text = "Ethereum Wallet Address:";
            // 
            // txtWalletAddress
            // 
            this.txtWalletAddress.Location = new System.Drawing.Point(144, 12);
            this.txtWalletAddress.MaxLength = 40;
            this.txtWalletAddress.Name = "txtWalletAddress";
            this.txtWalletAddress.Size = new System.Drawing.Size(216, 20);
            this.txtWalletAddress.TabIndex = 4;
            // 
            // chkText
            // 
            this.chkText.AutoSize = true;
            this.chkText.Location = new System.Drawing.Point(12, 38);
            this.chkText.Name = "chkText";
            this.chkText.Size = new System.Drawing.Size(340, 30);
            this.chkText.TabIndex = 5;
            this.chkText.Text = "Send a text message to the following phone number if one or more \r\nminers goes of" +
    "fline:";
            this.chkText.UseVisualStyleBackColor = true;
            this.chkText.CheckedChanged += new System.EventHandler(this.chkText_CheckedChanged);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Enabled = false;
            this.txtPhoneNumber.Location = new System.Drawing.Point(114, 68);
            this.txtPhoneNumber.MaxLength = 13;
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(172, 20);
            this.txtPhoneNumber.TabIndex = 6;
            this.txtPhoneNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneNumber_KeyPress);
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Enabled = false;
            this.lblPhoneNumber.Location = new System.Drawing.Point(27, 71);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(81, 13);
            this.lblPhoneNumber.TabIndex = 7;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Enabled = false;
            this.lblEmailAddress.Location = new System.Drawing.Point(27, 127);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(76, 13);
            this.lblEmailAddress.TabIndex = 10;
            this.lblEmailAddress.Text = "Email Address:";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Enabled = false;
            this.txtEmailAddress.Location = new System.Drawing.Point(114, 124);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(172, 20);
            this.txtEmailAddress.TabIndex = 9;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(12, 94);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(337, 30);
            this.chkEmail.TabIndex = 8;
            this.chkEmail.Text = "Send an email to the following email address if one or more miners \r\ngoes offline" +
    ":";
            this.chkEmail.UseVisualStyleBackColor = true;
            this.chkEmail.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // grpStatistics
            // 
            this.grpStatistics.Controls.Add(this.lblPrice);
            this.grpStatistics.Controls.Add(this.lblEarnings);
            this.grpStatistics.Controls.Add(this.lblBalance);
            this.grpStatistics.Enabled = false;
            this.grpStatistics.Location = new System.Drawing.Point(12, 150);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(348, 94);
            this.grpStatistics.TabIndex = 11;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "Statistics";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(3, 21);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(144, 25);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Current Price:";
            // 
            // lblEarnings
            // 
            this.lblEarnings.AutoSize = true;
            this.lblEarnings.Location = new System.Drawing.Point(8, 71);
            this.lblEarnings.Name = "lblEarnings";
            this.lblEarnings.Size = new System.Drawing.Size(143, 13);
            this.lblEarnings.TabIndex = 1;
            this.lblEarnings.Text = "Earnings in the last 24 hours:";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(8, 53);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(136, 13);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "Current Dwarfpool balance:";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 300);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.lblEmailAddress);
            this.Controls.Add(this.txtEmailAddress);
            this.Controls.Add(this.chkEmail);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.chkText);
            this.Controls.Add(this.txtWalletAddress);
            this.Controls.Add(this.lblWalletAddress);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.stsStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dwarfpool Mining Monitoring Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.stsStrip.ResumeLayout(false);
            this.stsStrip.PerformLayout();
            this.grpStatistics.ResumeLayout(false);
            this.grpStatistics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblWalletAddress;
        private System.Windows.Forms.CheckBox chkText;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblEmailAddress;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblEarnings;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox txtWalletAddress;
    }
}

