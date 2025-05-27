using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using VPN.Classes;
using System.Drawing.Drawing2D;

namespace VPN
{
    public partial class VerifyCodeForm : Form
    {
        private string expectedCode;
        private string email, username, password;

        public VerifyCodeForm(string code, string email, string username, string password)
        {
            this.expectedCode = code;
            this.email = email;
            this.username = username;
            this.password = password;

            InitializeComponent();
        }

        private void verifyBtn_Click(object sender, EventArgs e)
        {
            if (codeTxt.Text.Trim() == expectedCode)
            {
                if (UserStore.RegisterUser(email, username, password))
                {
                    CurrentUser.Email = email;
                    CurrentUser.Username = username;

                    MessageBox.Show("Registration successful!", "Success");

                    bool loginFormIsOpen = false;
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm is RegistrationForm || frm is VerifyCodeForm)
                            frm.Hide();
                        if (frm is LoginForm)
                            loginFormIsOpen = true;
                    }
                    if (!loginFormIsOpen)
                    {
                        var loginForm = new LoginForm();
                        loginForm.Show();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User already exists.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Invalid verification code.", "Error");
            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (codeTxt.Text == "Enter verification code")
            {
                codeTxt.Text = "";
                codeTxt.ForeColor = Color.White;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(codeTxt.Text))
            {
                codeTxt.Text = "Enter verification code";
                codeTxt.ForeColor = Color.FromArgb(225, 227, 232);
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
