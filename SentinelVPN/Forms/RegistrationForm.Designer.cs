using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VPN.LoginForm;

namespace VPN
{
    public partial class RegistrationForm : Form
    {
        private System.Windows.Forms.TextBox emailTxt;
        private System.Windows.Forms.TextBox usernameTxt;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.TextBox confirmTxt;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.Label confirmLbl;
        private System.Windows.Forms.Button ExitBtn, MiniBtn;
        private System.Windows.Forms.Label titleLbl;

        private void InitializeComponent()
        {
            this.Text = "Register";
            this.Size = new Size(325, 360);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(35, 39, 43);
            this.ForeColor = Color.White;
            this.Font = new Font("Verdana", 8.25F);
            this.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.app_icon));

            titleLbl = new Label()
            {
                Text = "Register",
                Font = new Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(0, 179, 135),
                Location = new Point(12, 9),
                AutoSize = true
            };

            ExitBtn = new Button()
            {
                Text = "X",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Tai Le", 11.25F),
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent,
                Size = new Size(29, 26),
                Location = new Point(this.Width - 40, 8),
                Cursor = Cursors.Hand
            };
            ExitBtn.FlatAppearance.BorderSize = 1;
            ExitBtn.Click += (s, e) => this.Close();

            MiniBtn = new Button()
            {
                Text = "_",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Tai Le", 11.25F),
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent,
                Size = new Size(29, 26),
                Location = new Point(this.Width - 73, 8),
                Cursor = Cursors.Hand
            };
            MiniBtn.FlatAppearance.BorderSize = 1;
            MiniBtn.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            var SeprLbl = new Label()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Size = new Size(this.Width - 8, 1),
                Location = new Point(4, 40)
            };

            int yOffset = 50;

            emailLbl = new Label()
            {
                Text = "Email",
                ForeColor = Color.Gray,
                Font = new Font("Verdana", 8),
                Left = 20,
                Top = yOffset
            };
            emailTxt = new TextBox()
            {
                Left = 20,
                Top = yOffset + 25,
                Width = 280,
                BackColor = Color.FromArgb(35, 39, 43),
                ForeColor = Color.FromArgb(225, 227, 232),
                BorderStyle = BorderStyle.FixedSingle
            };

            yOffset += 60;

            usernameLbl = new Label()
            {
                Text = "Username",
                ForeColor = Color.Gray,
                Font = new Font("Verdana", 8),
                Left = 20,
                Top = yOffset
            };
            usernameTxt = new TextBox()
            {
                Left = 20,
                Top = yOffset + 25,
                Width = 280,
                BackColor = Color.FromArgb(35, 39, 43),
                ForeColor = Color.FromArgb(225, 227, 232),
                BorderStyle = BorderStyle.FixedSingle
            };

            yOffset += 60;

            passwordLbl = new Label()
            {
                Text = "Password",
                ForeColor = Color.Gray,
                Font = new Font("Verdana", 8),
                Left = 20,
                Top = yOffset
            };
            passwordTxt = new TextBox()
            {
                Left = 20,
                Top = yOffset + 25,
                Width = 280,
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(35, 39, 43),
                ForeColor = Color.FromArgb(225, 227, 232),
                BorderStyle = BorderStyle.FixedSingle
            };

            yOffset += 60;

            confirmLbl = new Label()
            {
                Text = "Confirm Password",
                ForeColor = Color.Gray,
                Font = new Font("Verdana", 8),
                Left = 20,
                Top = yOffset
            };
            confirmTxt = new TextBox()
            {
                Left = 20,
                Top = yOffset + 25,
                Width = 280,
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(35, 39, 43),
                ForeColor = Color.FromArgb(225, 227, 232),
                BorderStyle = BorderStyle.FixedSingle
            };

            yOffset += 60;

            registerBtn = new Button()
            {
                Text = "Register",
                Top = yOffset + 10,
                Left = 20,
                Width = 280,
                Height = 35,
                Font = new Font("Verdana", 11F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent
            };
            registerBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);
            registerBtn.Click += new EventHandler(this.registerBtn_Click);

            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    NativeMethods.ReleaseCapture();
                    NativeMethods.SendMessage(this.Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HT_CAPTION, 0);
                }
            };

            this.Controls.Add(titleLbl);
            this.Controls.Add(ExitBtn);
            this.Controls.Add(MiniBtn);
            this.Controls.Add(SeprLbl);
            this.Controls.Add(emailLbl);
            this.Controls.Add(emailTxt);
            this.Controls.Add(usernameLbl);
            this.Controls.Add(usernameTxt);
            this.Controls.Add(passwordLbl);
            this.Controls.Add(passwordTxt);
            this.Controls.Add(confirmLbl);
            this.Controls.Add(confirmTxt);
            this.Controls.Add(registerBtn);
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
