using System.Drawing;
using System.Windows.Forms;
using static VPN.LoginForm;
using SentinelVPN.Properties;
using System.IO;

namespace VPN
{
    partial class VerifyCodeForm : Form
    {
        private TextBox codeTxt;
        private Button verifyBtn;
        private Button ExitBtn, MiniBtn;
        private Label titleLbl;

        private void InitializeComponent()
        {
            this.Text = "Verify Code";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(35, 39, 43);
            this.ClientSize = new Size(280, 145);
            this.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.app_icon));

            titleLbl = new Label()
            {
                Text = "Verification",
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

            codeTxt = new TextBox()
            {
                Text = "Enter verification code",
                ForeColor = Color.FromArgb(225, 227, 232),
                Font = new Font("Verdana", 8),
                BackColor = Color.FromArgb(35, 39, 43),
                BorderStyle = BorderStyle.FixedSingle,
                Top = 55,
                Left = 20,
                Width = 240
            };
            codeTxt.GotFocus += RemovePlaceholder;
            codeTxt.LostFocus += AddPlaceholder;

            verifyBtn = new Button()
            {
                Text = "Verify",
                Top = 95,
                Left = 20,
                Width = 240,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Verdana", 10),
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.Transparent
            };
            verifyBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);
            verifyBtn.Click += verifyBtn_Click;

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
            this.Controls.Add(codeTxt);
            this.Controls.Add(verifyBtn);
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
