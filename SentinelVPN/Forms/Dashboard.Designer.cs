using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VPN
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.ExitBtn = new System.Windows.Forms.Button();
            this.MiniBtn = new System.Windows.Forms.Button();
            this.SeprLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.CountriesCmBox = new System.Windows.Forms.ComboBox();
            this.CountriesFlgPicBox = new System.Windows.Forms.PictureBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.openApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.InfoBtn = new System.Windows.Forms.Button();
            this.ProtocolGrpBox = new System.Windows.Forms.GroupBox();
            this.PPTP_rBtn = new ModernRadioButton();
            this.OpenVPN_rBtn = new ModernRadioButton();
            this.L2TP_rBtn = new ModernRadioButton();
            this.WireGuard_rBtn = new ModernRadioButton();
            this.VLESS_rBtn = new ModernRadioButton();
            this.PingLbl = new System.Windows.Forms.Label();
            this.DomainTxtBox = new System.Windows.Forms.TextBox();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.DomainsListBox = new System.Windows.Forms.ListBox();
            this.AddDomainBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CountriesFlgPicBox)).BeginInit();
            this.CMenu.SuspendLayout();
            this.ProtocolGrpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExitBtn
            // 
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.ExitBtn.Location = new System.Drawing.Point(377, 8);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(29, 26);
            this.ExitBtn.TabIndex = 0;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // MiniBtn
            // 
            this.MiniBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MiniBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiniBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiniBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.MiniBtn.Location = new System.Drawing.Point(343, 8);
            this.MiniBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MiniBtn.Name = "MiniBtn";
            this.MiniBtn.Size = new System.Drawing.Size(29, 26);
            this.MiniBtn.TabIndex = 1;
            this.MiniBtn.Text = "_";
            this.MiniBtn.UseVisualStyleBackColor = true;
            this.MiniBtn.Click += new System.EventHandler(this.MiniBtn_Click);
            // 
            // SeprLbl
            // 
            this.SeprLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeprLbl.Location = new System.Drawing.Point(4, 40);
            this.SeprLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeprLbl.Name = "SeprLbl";
            this.SeprLbl.Size = new System.Drawing.Size(407, 1);
            this.SeprLbl.TabIndex = 2;
            //
            // Logo PictureBox setup
            //
            this.LogoPicBox = new System.Windows.Forms.PictureBox();
            this.LogoPicBox.Location = new System.Drawing.Point(8, 5);
            this.LogoPicBox.Size = new System.Drawing.Size(30, 30);
            this.LogoPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            using (var ms = new MemoryStream(SentinelVPN.Properties.Resources.app_icon))
            {
                this.LogoPicBox.Image = Image.FromStream(ms);
            }
            this.LogoPicBox.BackColor = Color.Transparent;
            this.LogoPicBox.TabIndex = 100;
            this.LogoPicBox.TabStop = false;
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.NameLbl.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.NameLbl.Location = new System.Drawing.Point(36, 9);
            this.NameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(126, 20);
            this.NameLbl.TabIndex = 3;
            this.NameLbl.Text = "SentinelVPN";
            this.NameLbl.Click += new System.EventHandler(this.NameLbl_Click);
            // 
            // CountriesCmBox
            // 
            this.CountriesCmBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.CountriesCmBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CountriesCmBox.DropDownHeight = 95;
            this.CountriesCmBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CountriesCmBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CountriesCmBox.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountriesCmBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.CountriesCmBox.FormattingEnabled = true;
            this.CountriesCmBox.IntegralHeight = false;
            this.CountriesCmBox.ItemHeight = 23;
            this.CountriesCmBox.Location = new System.Drawing.Point(14, 132);
            this.CountriesCmBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CountriesCmBox.Name = "CountriesCmBox";
            this.CountriesCmBox.Size = new System.Drawing.Size(246, 31);
            this.CountriesCmBox.TabIndex = 4;
            this.CountriesCmBox.SelectedIndexChanged += new System.EventHandler(this.CountriesCmBox_SelectedIndexChanged);
            // 
            // CountriesFlgPicBox
            // 
            this.CountriesFlgPicBox.Location = new System.Drawing.Point(267, 132);
            this.CountriesFlgPicBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CountriesFlgPicBox.Name = "CountriesFlgPicBox";
            this.CountriesFlgPicBox.Size = new System.Drawing.Size(56, 31);
            this.CountriesFlgPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CountriesFlgPicBox.TabIndex = 5;
            this.CountriesFlgPicBox.TabStop = false;
            this.CountriesFlgPicBox.Click += new System.EventHandler(this.CountriesFlgPicBox_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectBtn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.ConnectBtn.Location = new System.Drawing.Point(210, 169);
            this.ConnectBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(189, 43);
            this.ConnectBtn.TabIndex = 6;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisconnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectBtn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.DisconnectBtn.Location = new System.Drawing.Point(14, 169);
            this.DisconnectBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(189, 43);
            this.DisconnectBtn.TabIndex = 7;
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.BalloonTipText = "Running in background";
            this.NotifyIcon.BalloonTipTitle = "SentinelVPN";
            this.NotifyIcon.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.NotifyIcon_Icon));
            this.NotifyIcon.Text = "SentinelVPN";
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // openApplicationToolStripMenuItem
            // 
            this.openApplicationToolStripMenuItem.Name = "openApplicationToolStripMenuItem";
            this.openApplicationToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openApplicationToolStripMenuItem.Text = "Open Application";
            this.openApplicationToolStripMenuItem.Click += new System.EventHandler(this.openApplicationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // CMenu
            // 
            this.CMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openApplicationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.CMenu.Name = "CMenu";
            this.CMenu.Size = new System.Drawing.Size(168, 48);
            // 
            // InfoBtn
            // 
            this.InfoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoBtn.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.InfoBtn.Location = new System.Drawing.Point(309, 8);
            this.InfoBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InfoBtn.Name = "InfoBtn";
            this.InfoBtn.Size = new System.Drawing.Size(29, 26);
            this.InfoBtn.TabIndex = 12;
            this.InfoBtn.Text = "?";
            this.InfoBtn.UseVisualStyleBackColor = true;
            this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
            // 
            // ProtocolGrpBox
            // 
            this.ProtocolGrpBox.Controls.Add(this.PPTP_rBtn);
            this.ProtocolGrpBox.Controls.Add(this.OpenVPN_rBtn);
            this.ProtocolGrpBox.Controls.Add(this.L2TP_rBtn);
            this.ProtocolGrpBox.Controls.Add(this.WireGuard_rBtn);
            this.ProtocolGrpBox.Controls.Add(this.VLESS_rBtn);
            this.ProtocolGrpBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProtocolGrpBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.ProtocolGrpBox.Location = new System.Drawing.Point(14, 46);
            this.ProtocolGrpBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProtocolGrpBox.Name = "ProtocolGrpBox";
            this.ProtocolGrpBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProtocolGrpBox.Size = new System.Drawing.Size(390, 80);
            this.ProtocolGrpBox.TabIndex = 14;
            this.ProtocolGrpBox.TabStop = false;
            this.ProtocolGrpBox.Text = "Protocol";
            // 
            // PPTP_rBtn
            // 
            this.PPTP_rBtn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.PPTP_rBtn.AutoSize = true;
            this.PPTP_rBtn.BackColor = System.Drawing.Color.Transparent;
            this.PPTP_rBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PPTP_rBtn.DotSize = 8;
            this.PPTP_rBtn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PPTP_rBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.PPTP_rBtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.PPTP_rBtn.Location = new System.Drawing.Point(87, 14);
            this.PPTP_rBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PPTP_rBtn.Name = "PPTP_rBtn";
            this.PPTP_rBtn.OuterSize = 18;
            this.PPTP_rBtn.Size = new System.Drawing.Size(77, 27);
            this.PPTP_rBtn.TabIndex = 13;
            this.PPTP_rBtn.Text = "PPTP";
            this.PPTP_rBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.PPTP_rBtn.UseVisualStyleBackColor = true;
            this.PPTP_rBtn.CheckedChanged += new System.EventHandler(this.PPTP_rBtn_CheckedChanged);
            // 
            // OpenVPN_rBtn
            // 
            this.OpenVPN_rBtn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.OpenVPN_rBtn.AutoSize = true;
            this.OpenVPN_rBtn.BackColor = System.Drawing.Color.Transparent;
            this.OpenVPN_rBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenVPN_rBtn.DotSize = 8;
            this.OpenVPN_rBtn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenVPN_rBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.OpenVPN_rBtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.OpenVPN_rBtn.Location = new System.Drawing.Point(8, 47);
            this.OpenVPN_rBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OpenVPN_rBtn.Name = "OpenVPN_rBtn";
            this.OpenVPN_rBtn.OuterSize = 18;
            this.OpenVPN_rBtn.Size = new System.Drawing.Size(120, 27);
            this.OpenVPN_rBtn.TabIndex = 21;
            this.OpenVPN_rBtn.Text = "OpenVPN";
            this.OpenVPN_rBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.OpenVPN_rBtn.UseVisualStyleBackColor = false;
            this.OpenVPN_rBtn.CheckedChanged += new System.EventHandler(this.OpenVPN_rBtn_CheckedChanged);
            // 
            // L2TP_rBtn
            // 
            this.L2TP_rBtn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.L2TP_rBtn.AutoSize = true;
            this.L2TP_rBtn.BackColor = System.Drawing.Color.Transparent;
            this.L2TP_rBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L2TP_rBtn.DotSize = 8;
            this.L2TP_rBtn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L2TP_rBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.L2TP_rBtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.L2TP_rBtn.Location = new System.Drawing.Point(235, 14);
            this.L2TP_rBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.L2TP_rBtn.Name = "L2TP_rBtn";
            this.L2TP_rBtn.OuterSize = 18;
            this.L2TP_rBtn.Size = new System.Drawing.Size(78, 27);
            this.L2TP_rBtn.TabIndex = 14;
            this.L2TP_rBtn.Text = "L2TP";
            this.L2TP_rBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.L2TP_rBtn.UseVisualStyleBackColor = false;
            this.L2TP_rBtn.CheckedChanged += new System.EventHandler(this.L2TP_rBtn_CheckedChanged);
            // 
            // WireGuard_rBtn
            // 
            this.WireGuard_rBtn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.WireGuard_rBtn.AutoSize = true;
            this.WireGuard_rBtn.BackColor = System.Drawing.Color.Transparent;
            this.WireGuard_rBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WireGuard_rBtn.DotSize = 8;
            this.WireGuard_rBtn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WireGuard_rBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.WireGuard_rBtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.WireGuard_rBtn.Location = new System.Drawing.Point(142, 47);
            this.WireGuard_rBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WireGuard_rBtn.Name = "WireGuard_rBtn";
            this.WireGuard_rBtn.OuterSize = 18;
            this.WireGuard_rBtn.Size = new System.Drawing.Size(134, 27);
            this.WireGuard_rBtn.TabIndex = 22;
            this.WireGuard_rBtn.Text = "WireGuard";
            this.WireGuard_rBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.WireGuard_rBtn.UseVisualStyleBackColor = false;
            this.WireGuard_rBtn.CheckedChanged += new System.EventHandler(this.WireGuard_rBtn_CheckedChanged);
            // 
            // VLESS_rBtn
            // 
            this.VLESS_rBtn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.VLESS_rBtn.AutoSize = true;
            this.VLESS_rBtn.BackColor = System.Drawing.Color.Transparent;
            this.VLESS_rBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VLESS_rBtn.DotSize = 8;
            this.VLESS_rBtn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLESS_rBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.VLESS_rBtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.VLESS_rBtn.Location = new System.Drawing.Point(291, 47);
            this.VLESS_rBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VLESS_rBtn.Name = "VLESS_rBtn";
            this.VLESS_rBtn.OuterSize = 18;
            this.VLESS_rBtn.Size = new System.Drawing.Size(94, 27);
            this.VLESS_rBtn.TabIndex = 23;
            this.VLESS_rBtn.Text = "VLESS";
            this.VLESS_rBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.VLESS_rBtn.UseVisualStyleBackColor = false;
            this.VLESS_rBtn.CheckedChanged += new System.EventHandler(this.VLESS_rBtn_CheckedChanged);
            // 
            // PingLbl
            // 
            this.PingLbl.Font = new System.Drawing.Font("Verdana", 12F);
            this.PingLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.PingLbl.Location = new System.Drawing.Point(328, 133);
            this.PingLbl.Name = "PingLbl";
            this.PingLbl.Size = new System.Drawing.Size(83, 31);
            this.PingLbl.TabIndex = 1;
            this.PingLbl.Text = "0 ms";
            this.PingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DomainTxtBox
            // 
            this.DomainTxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.DomainTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DomainTxtBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.DomainTxtBox.Location = new System.Drawing.Point(70, 227);
            this.DomainTxtBox.Name = "DomainTxtBox";
            this.DomainTxtBox.Size = new System.Drawing.Size(222, 21);
            this.DomainTxtBox.TabIndex = 18;
            this.DomainTxtBox.Text = "example.com";
            this.DomainTxtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DomainTxtBox_KeyDown);
            // 
            // StatusLbl
            // 
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.StatusLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(97)))), ((int)(((byte)(105)))));
            this.StatusLbl.Location = new System.Drawing.Point(153, 315);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(111, 17);
            this.StatusLbl.TabIndex = 99;
            this.StatusLbl.Text = "Disconnected";
            this.StatusLbl.Click += new System.EventHandler(this.StatusLbl_Click);
            // 
            // DomainsListBox
            // 
            this.DomainsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.DomainsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DomainsListBox.Font = new System.Drawing.Font("Verdana", 8F);
            this.DomainsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.DomainsListBox.Location = new System.Drawing.Point(70, 255);
            this.DomainsListBox.Name = "DomainsListBox";
            this.DomainsListBox.Size = new System.Drawing.Size(271, 54);
            this.DomainsListBox.TabIndex = 20;
            this.DomainsListBox.DoubleClick += new System.EventHandler(this.DomainsListBox_DoubleClick);
            // 
            // AddDomainBtn
            // 
            this.AddDomainBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddDomainBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddDomainBtn.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.AddDomainBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.AddDomainBtn.Location = new System.Drawing.Point(300, 227);
            this.AddDomainBtn.Name = "AddDomainBtn";
            this.AddDomainBtn.Size = new System.Drawing.Size(40, 20);
            this.AddDomainBtn.TabIndex = 19;
            this.AddDomainBtn.Text = "->";
            this.AddDomainBtn.UseVisualStyleBackColor = true;
            this.AddDomainBtn.Click += new System.EventHandler(this.AddDomainBtn_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(417, 340);
            this.ControlBox = false;
            this.Controls.Add(this.PingLbl);
            this.Controls.Add(this.ProtocolGrpBox);
            this.Controls.Add(this.InfoBtn);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.DomainTxtBox);
            this.Controls.Add(this.AddDomainBtn);
            this.Controls.Add(this.DomainsListBox);
            this.Controls.Add(this.CountriesFlgPicBox);
            this.Controls.Add(this.CountriesCmBox);
            this.Controls.Add(this.LogoPicBox);
            this.Controls.Add(this.NameLbl);
            this.Controls.Add(this.SeprLbl);
            this.Controls.Add(this.StatusLbl);
            this.Controls.Add(this.MiniBtn);
            this.Controls.Add(this.ExitBtn);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.app_icon));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SentinelVPN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.CountriesFlgPicBox)).EndInit();
            this.CMenu.ResumeLayout(false);
            this.ProtocolGrpBox.ResumeLayout(false);
            this.ProtocolGrpBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button MiniBtn;
        private System.Windows.Forms.Label SeprLbl;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.ComboBox CountriesCmBox;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.TextBox DomainTxtBox;
        private System.Windows.Forms.ListBox DomainsListBox;
        private System.Windows.Forms.Button AddDomainBtn;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem openApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip CMenu;
        private System.Windows.Forms.Button InfoBtn;
        private System.Windows.Forms.GroupBox ProtocolGrpBox;
        private System.Windows.Forms.PictureBox LogoPicBox;
        private System.Windows.Forms.PictureBox CountriesFlgPicBox;
        private System.Windows.Forms.Label PingLbl;
        private ModernRadioButton PPTP_rBtn;
        private ModernRadioButton OpenVPN_rBtn;
        private ModernRadioButton L2TP_rBtn;
        private ModernRadioButton WireGuard_rBtn;
        private ModernRadioButton VLESS_rBtn;

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

        public void DisconnectBtnEnabledFalse()
        {
            DisconnectBtn.Enabled = false;
            DisconnectBtn.ForeColor = Color.FromArgb(93, 97, 105);
            DisconnectBtn.BackColor = Color.FromArgb(93, 97, 105);
        }

        public void DisconnectBtnEnabledTrue()
        {
            DisconnectBtn.Enabled = true;
            DisconnectBtn.ForeColor = Color.FromArgb(0, 179, 135);
            DisconnectBtn.BackColor = Color.Transparent;
        }

        public void ConnectBtnEnabledFalse()
        {
            ConnectBtn.Enabled = false;
            ConnectBtn.ForeColor = Color.FromArgb(93, 97, 105);
            ConnectBtn.BackColor = Color.FromArgb(93, 97, 105);
        }

        public void ConnectBtnEnabledTrue()
        {
            ConnectBtn.Enabled = true;
            ConnectBtn.ForeColor = Color.FromArgb(0, 179, 135);
            ConnectBtn.BackColor = Color.Transparent;
        }

        public void CountriesCmBoxEnabledFalse()
        {
            CountriesCmBox.Enabled = false;
            CountriesCmBox.ForeColor = Color.FromArgb(93, 97, 105);
            CountriesCmBox.BackColor = Color.FromArgb(93, 97, 105);
        }

        public void CountriesCmBoxEnabledTrue()
        {
            CountriesCmBox.Enabled = true;
            CountriesCmBox.ForeColor = Color.FromArgb(0, 179, 135);
            CountriesCmBox.BackColor = Color.FromArgb(35, 39, 43);
        }

        public void ImageLoader(string locationName)
        {
            if (locationName.Contains("FR"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.FranceFlg;
            else if (locationName.Contains("CA"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.CanadaFlg;
            else if (locationName.Contains("DE"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.GermanyFlg;
            else if (locationName.Contains("SG"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.SingaporeFlg;
            else if (locationName.Contains("NL"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.NetherlandsFlg;
            else if (locationName.Contains("US"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.UsaFlg;
            else if (locationName.Contains("RU"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.RussiaFlg;
            else if (locationName.Contains("JP"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.JapanFlg;
            else if (locationName.Contains("BR"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.BrazilFlg;
            else if (locationName.Contains("UK"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.UkFlg;
            else if (locationName.Contains("SE"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.SwedenFlg;
            else if (locationName.Contains("PL"))
                CountriesFlgPicBox.Image = SentinelVPN.Properties.Resources.PolandFlg;
        }
    }
}