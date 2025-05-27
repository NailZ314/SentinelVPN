using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using VPN.Classes;

namespace VPN
{
    public partial class LoginForm : Form
    {
        private const string RememberFile = "remember.dat";

        public LoginForm()
        {
            InitializeComponent();
            LoadRememberedCredentials();
            AutoLoginIfPossible();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string email = emailTxt.Text.Trim();
            string password = passwordTxt.Text;

            if (UserStore.ValidateUser(email, password))
            {
                CurrentUser.Email = email;
                CurrentUser.Username = UserStore.GetUsername(email);

                if (rememberMeChk.Checked)
                {
                    string content = email + ":" + password;
                    File.WriteAllText(RememberFile, Encrypt(content));
                }
                else if (File.Exists(RememberFile))
                {
                    File.Delete(RememberFile);
                }

                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Error");
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }

        private void LoadRememberedCredentials()
        {
            if (File.Exists(RememberFile))
            {
                try
                {
                    string decrypted = Decrypt(File.ReadAllText(RememberFile));
                    string[] parts = decrypted.Split(':');
                    if (parts.Length == 2)
                    {
                        emailTxt.Text = parts[0];
                        passwordTxt.Text = parts[1];
                        rememberMeChk.Checked = true;
                    }
                }
                catch { }
            }
        }

        private void AutoLoginIfPossible()
        {
            if (rememberMeChk.Checked)
            {
                loginBtn.PerformClick();
            }
        }

        private string Encrypt(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string encryptedText)
        {
            byte[] data = Convert.FromBase64String(encryptedText);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }

        internal class NativeMethods
        {
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Graphics g = e.Graphics)
            using (Pen borderPen = new Pen(Color.FromArgb(20, 255, 255, 255), 1))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int radius = 8;
                Rectangle bounds = new Rectangle(0, 0, this.Width - 2, this.Height - 2);

                using (GraphicsPath path = GetRoundedRectPath(bounds, radius))
                {
                    g.DrawPath(borderPen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius == 0)
            {
                path.AddRectangle(bounds);
            }
            else
            {
                path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
            }

            return path;
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 0, Height - 0, 15, 15));

        }
    }
}