using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VPN
{
    public partial class RegistrationForm : Form
    {
        private string verificationCode;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string email = emailTxt.Text.Trim();
            string username = usernameTxt.Text.Trim();
            string password = passwordTxt.Text;
            string confirm = confirmTxt.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show("All fields are required.", "Validation Error");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error");
                return;
            }

            if (UserStore.UserExistsByEmail(email))
            {
                MessageBox.Show("This email is already registered.", "Validation Error");
                return;
            }

            if (UserStore.UserExistsByUsername(username))
            {
                MessageBox.Show("This username is already taken.", "Validation Error");
                return;
            }

            verificationCode = new Random().Next(100000, 999999).ToString();
            bool sent = EmailHelper.SendVerificationCode(email, verificationCode);
            if (!sent) return;

            using (var verifyForm = new VerifyCodeForm(verificationCode, email, username, password))
            {
                verifyForm.ShowDialog();
            }
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
