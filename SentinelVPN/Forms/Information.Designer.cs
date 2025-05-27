using System.Windows.Forms;
using LiveCharts.WinForms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Drawing;

namespace VPN
{
    partial class Information
    {
        private System.ComponentModel.IContainer components = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Information));
            this.SeprLbl = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SubLblStatic = new System.Windows.Forms.Label();
            this.SubLblDyn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SeprLbl
            // 
            this.SeprLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeprLbl.Location = new System.Drawing.Point(4, 40);
            this.SeprLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeprLbl.Name = "SeprLbl";
            this.SeprLbl.Size = new System.Drawing.Size(410, 1);
            this.SeprLbl.TabIndex = 3;
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(44)))));
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.ExitBtn.Location = new System.Drawing.Point(380, 8);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(29, 26);
            this.ExitBtn.TabIndex = 4;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // MiniBtn
            // 
            this.MiniBtn = new System.Windows.Forms.Button();
            this.MiniBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(44)))));
            this.MiniBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MiniBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiniBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiniBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.MiniBtn.Location = new System.Drawing.Point(347, 8);
            this.MiniBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MiniBtn.Name = "MiniBtn";
            this.MiniBtn.Size = new System.Drawing.Size(29, 26);
            this.MiniBtn.TabIndex = 15;
            this.MiniBtn.Text = "_";
            this.MiniBtn.UseVisualStyleBackColor = false;
            this.MiniBtn.Click += new System.EventHandler(this.MiniBtn_Click);
            // 
            // SubLblStatic
            // 
            this.SubLblStatic.AutoSize = true;
            this.SubLblStatic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(44)))));
            this.SubLblStatic.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubLblStatic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.SubLblStatic.Location = new System.Drawing.Point(12, 9);
            this.SubLblStatic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SubLblStatic.Name = "SubLblStatic";
            this.SubLblStatic.Size = new System.Drawing.Size(142, 20);
            this.SubLblStatic.TabIndex = 11;
            this.SubLblStatic.Text = "Info";
            this.SubLblStatic.Click += new System.EventHandler(this.SubLblStatic_Click);
            // 
            // SubLblDyn
            // 
            this.SubLblDyn.AutoSize = true;
            this.SubLblDyn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubLblDyn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.SubLblDyn.Location = new System.Drawing.Point(18, 54);
            this.SubLblDyn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SubLblDyn.Name = "SubLblDyn";
            this.SubLblDyn.Size = new System.Drawing.Size(0, 16);
            this.SubLblDyn.TabIndex = 12;
            // 
            // LogoutBtn
            // 
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutBtn.Font = new System.Drawing.Font("Verdana", 12F);
            this.LogoutBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.LogoutBtn.BackColor = System.Drawing.Color.Transparent;
            this.LogoutBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.LogoutBtn.FlatAppearance.BorderSize = 1;
            this.LogoutBtn.Location = new System.Drawing.Point(215, 82);
            this.LogoutBtn.Name = "logoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(189, 43);
            this.LogoutBtn.TabIndex = 13;
            this.LogoutBtn.Text = "Log Out";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // RenewBtn
            // 
            this.RenewBtn = new System.Windows.Forms.Button();
            this.RenewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RenewBtn.Font = new System.Drawing.Font("Verdana", 12F);
            this.RenewBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.RenewBtn.BackColor = System.Drawing.Color.Transparent;
            this.RenewBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.RenewBtn.FlatAppearance.BorderSize = 1;
            this.RenewBtn.Location = new System.Drawing.Point(20, 82);
            this.RenewBtn.Name = "renewBtn";
            this.RenewBtn.Size = new System.Drawing.Size(189, 43);
            this.RenewBtn.TabIndex = 14;
            this.RenewBtn.Text = "Renew Subscription";
            this.RenewBtn.UseVisualStyleBackColor = false;
            this.RenewBtn.Click += new System.EventHandler(this.RenewBtn_Click);
            // 
            // SpeedTestBtn
            // 
            this.SpeedTestBtn = new System.Windows.Forms.Button();
            this.SpeedTestBtn.Text = "Run Speed Test";
            this.SpeedTestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpeedTestBtn.Font = new System.Drawing.Font("Verdana", 12F);
            this.SpeedTestBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.SpeedTestBtn.BackColor = System.Drawing.Color.Transparent;
            this.SpeedTestBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.SpeedTestBtn.FlatAppearance.BorderSize = 1;
            this.SpeedTestBtn.Location = new System.Drawing.Point(20, 130);
            this.SpeedTestBtn.Name = "SpeedTestBtn";
            this.SpeedTestBtn.Size = new System.Drawing.Size(189, 43);
            this.SpeedTestBtn.TabIndex = 14;
            this.SpeedTestBtn.Click += new System.EventHandler(this.SpeedTestBtn_Click);
            // 
            // StopSpeedTestBtn
            //
            this.StopSpeedTestBtn = new System.Windows.Forms.Button();
            this.StopSpeedTestBtn.Text = "Stop Speed Test";
            this.StopSpeedTestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopSpeedTestBtn.Font = new System.Drawing.Font("Verdana", 12F);
            this.StopSpeedTestBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.StopSpeedTestBtn.BackColor = System.Drawing.Color.Transparent;
            this.StopSpeedTestBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.StopSpeedTestBtn.FlatAppearance.BorderSize = 1;
            this.StopSpeedTestBtn.Location = new System.Drawing.Point(215, 130);
            this.StopSpeedTestBtn.Name = "StopSpeedTestBtn";
            this.StopSpeedTestBtn.Size = new System.Drawing.Size(189, 43);
            this.StopSpeedTestBtn.TabIndex = 14;
            this.StopSpeedTestBtn.Click += new System.EventHandler(this.StopSpeedTestBtn_Click);
            // 
            // SpeedChart
            // 
            this.SpeedChart = new LiveCharts.WinForms.CartesianChart();
            this.SpeedChart.Location = new System.Drawing.Point(10, 260);
            this.SpeedChart.Size = new System.Drawing.Size(380, 200);
            this.SpeedChart.Name = "Speed Chart";
            this.SpeedChart.BackColor = System.Drawing.Color.FromArgb(35, 39, 43);
            this.SpeedChart.Visible = false;
            // 
            // IpLabel
            //
            this.IpLabel = new System.Windows.Forms.Label();
            this.IpLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IpLabel.ForeColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.IpLabel.Location = new System.Drawing.Point(18, 180);
            this.IpLabel.Size = new System.Drawing.Size(380, 20);
            this.IpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.IpLabel.Name = "IpLabel";
            this.IpLabel.TabIndex = 12;
            this.IpLabel.Visible = true;
            //
            // Tunnel IP Label
            //
            this.TunIpLabel = new System.Windows.Forms.Label();
            this.TunIpLabel.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.TunIpLabel.ForeColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.TunIpLabel.Location = new System.Drawing.Point(18, 220);
            this.TunIpLabel.Size = new System.Drawing.Size(380, 20);
            this.TunIpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TunIpLabel.Name = "TunIpLabel";
            this.TunIpLabel.TabIndex = 13;
            this.TunIpLabel.Visible = true;
            //
            // DNS Label
            //
            this.DnsLabel = new System.Windows.Forms.Label();
            this.DnsLabel.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DnsLabel.ForeColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.DnsLabel.Location = new System.Drawing.Point(18, 200);
            this.DnsLabel.Size = new System.Drawing.Size(380, 20);
            this.DnsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DnsLabel.Name = "DnsLabel";
            this.DnsLabel.TabIndex = 14;
            this.DnsLabel.Visible = true;
            //
            // Latency Label
            //
            this.LatencyLabel = new System.Windows.Forms.Label();
            this.LatencyLabel.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.LatencyLabel.ForeColor = System.Drawing.Color.FromArgb(0, 179, 135);
            this.LatencyLabel.Location = new System.Drawing.Point(18, 240);
            this.LatencyLabel.Size = new System.Drawing.Size(380, 20);
            this.LatencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LatencyLabel.Name = "LatencyLabel";
            this.LatencyLabel.TabIndex = 15;
            this.LatencyLabel.Visible = true;
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(420, 270);
            this.ControlBox = false;
            this.Controls.Add(this.SubLblDyn);
            this.Controls.Add(this.SubLblStatic);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.MiniBtn);
            this.Controls.Add(this.SeprLbl);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.RenewBtn);
            this.Controls.Add(this.SpeedTestBtn);
            this.Controls.Add(this.StopSpeedTestBtn);
            this.Controls.Add(this.SpeedChart);
            this.Controls.Add(this.IpLabel);
            this.Controls.Add(this.TunIpLabel);
            this.Controls.Add(this.DnsLabel);
            this.Controls.Add(this.LatencyLabel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Info";
            this.Text = "Information";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Information_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Information_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SeprLbl;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button MiniBtn;
        private System.Windows.Forms.Label SubLblStatic;
        private System.Windows.Forms.Label SubLblDyn;
        private System.Windows.Forms.Label IpLabel;
        private System.Windows.Forms.Label TunIpLabel;
        private System.Windows.Forms.Label DnsLabel;
        private System.Windows.Forms.Label LatencyLabel;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.Button RenewBtn;
        private System.Windows.Forms.Button SpeedTestBtn;
        private System.Windows.Forms.Button StopSpeedTestBtn;
        private LiveCharts.WinForms.CartesianChart SpeedChart;

        public void StartSpeedTestBtnEnabledFalse()
        {
            SpeedTestBtn.Enabled = false;
            SpeedTestBtn.ForeColor = Color.FromArgb(93, 97, 105);
            SpeedTestBtn.BackColor = Color.FromArgb(93, 97, 105);
        }

        public void StartSpeedTestBtnEnabledTrue()
        {
            SpeedTestBtn.Enabled = true;
            SpeedTestBtn.ForeColor = Color.FromArgb(0, 179, 135);
            SpeedTestBtn.BackColor = Color.Transparent;
        }

        public void StopSpeedTestBtnEnabledFalse()
        {
            StopSpeedTestBtn.Enabled = false;
            StopSpeedTestBtn.ForeColor = Color.FromArgb(93, 97, 105);
            StopSpeedTestBtn.BackColor = Color.FromArgb(93, 97, 105);
        }

        public void StopSpeedTestBtnEnabledTrue()
        {
            StopSpeedTestBtn.Enabled = true;
            StopSpeedTestBtn.ForeColor = Color.FromArgb(0, 179, 135);
            StopSpeedTestBtn.BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
    }
}