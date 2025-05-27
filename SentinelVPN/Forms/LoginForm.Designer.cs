using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace VPN
{
    public partial class LoginForm : Form
    {
        private TextBox emailTxt;
        private TextBox passwordTxt;
        private CheckBox rememberMeChk;
        private Button loginBtn;
        private Button registerBtn;

        private void InitializeComponent()
        {
            this.Text = "Login";
            this.Size = new Size(305, 305);
            this.BackColor = Color.FromArgb(35, 39, 43);
            this.ForeColor = Color.FromArgb(225, 227, 232);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.app_icon));

            var titleLbl = new Label()
            {
                Text = "Login",
                Font = new Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(12, 9)
            };

            var ExitBtn = new Button()
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

            var MiniBtn = new Button()
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

            var emailLbl = new Label()
            {
                Text = "Email",
                ForeColor = Color.FromArgb(225, 227, 232),
                Font = new Font("Verdana", 8F),
                Left = 17,
                Top = 50,
                Width = 100,
            };
            emailTxt = new TextBox()
            {
                Top = 75,
                Left = 20,
                Width = 260,
                BackColor = Color.FromArgb(35, 39, 43),
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.FromArgb(225, 227, 232)
            };

            var passwordLbl = new Label()
            {
                Text = "Password",
                ForeColor = Color.FromArgb(225, 227, 232),
                Font = new Font("Verdana", 8F),
                Left = 17,
                Top = 110,
                Width = 100,
            };
            passwordTxt = new TextBox()
            {
                Top = 135,
                Left = 20,
                Width = 260,
                BackColor = Color.FromArgb(35, 39, 43),
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.FromArgb(225, 227, 232),
                UseSystemPasswordChar = true
            };

            rememberMeChk = new ModernCheckBox()
            {
                Text = "Remember Me",
                Top = 170,
                Left = 20,
                AccentColor = Color.FromArgb(0, 179, 135),
                TextColor = Color.FromArgb(170, 173, 182),
                Font = new Font("Verdana", 8.25F, FontStyle.Regular),
                AutoSize = true
            };

            loginBtn = new Button()
            {
                Text = "Login",
                Top = 205,
                Left = 20,
                Width = 260,
                Height = 35,
                Font = new Font("Verdana", 11F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent
            };
            loginBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);
            loginBtn.Click += loginBtn_Click;

            registerBtn = new Button()
            {
                Text = "Register",
                Top = 255,
                Left = 20,
                Width = 260,
                Height = 35,
                Font = new Font("Verdana", 11F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent
            };
            registerBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);
            registerBtn.Click += registerBtn_Click;

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
            this.Controls.Add(passwordLbl);
            this.Controls.Add(passwordTxt);
            this.Controls.Add(rememberMeChk);
            this.Controls.Add(loginBtn);
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
