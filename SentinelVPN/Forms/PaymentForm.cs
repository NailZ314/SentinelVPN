using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VPN.Classes;

namespace VPN
{
    public partial class PaymentForm : Form
    {
        private TextBox cardNumberTxt, expiryTxt, cvvTxt;
        private Label cardLbl, expiryLbl, cvvLbl, confirmationLbl;
        private Button[] durationButtons;
        private Label[] priceLabels;
        private int[] durations = { 7, 14, 30, 180, 360 };
        private int[] prices = { 500, 900, 1500, 6000, 10000 };

        public PaymentForm()
        {
            InitializeComponent();
        }

        private int selectedDays = 0;

        private void CardNumberTxt_TextChanged(object sender, EventArgs e)
        {
            string clean = new string(cardNumberTxt.Text.Where(char.IsDigit).ToArray());
            if (clean.Length > 16) clean = clean.Substring(0, 16);

            cardNumberTxt.Text = string.Join(" ", Enumerable.Range(0, clean.Length / 4 + (clean.Length % 4 == 0 ? 0 : 1))
                .Select(i => clean.Substring(i * 4, Math.Min(4, clean.Length - i * 4))));
            cardNumberTxt.SelectionStart = cardNumberTxt.Text.Length;
        }

        private void ExpiryTxt_TextChanged(object sender, EventArgs e)
        {
            string clean = new string(expiryTxt.Text.Where(c => char.IsDigit(c) || c == '/').ToArray());

            if (clean.Length == 2 && !clean.Contains("/"))
            {
                clean += "/";
            }

            if (clean.Length > 5)
                clean = clean.Substring(0, 5);

            expiryTxt.Text = clean;
            expiryTxt.SelectionStart = expiryTxt.Text.Length;
        }

        private void OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void OnlyDigitsAndSlash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
                e.Handled = true;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (cardNumberTxt.Text.Replace(" ", "").Length != 16 ||
                expiryTxt.Text.Length != 5 || !expiryTxt.Text.Contains("/") ||
                cvvTxt.Text.Length != 3 || selectedDays == 0)
            {
                MessageBox.Show("Please enter valid card info and select a subscription.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(CurrentUser.Email))
            {
                MessageBox.Show("User not logged in.", "Error");
                return;
            }

            SubscriptionManager.AddSubscriptionDays(CurrentUser.Email, selectedDays);
            var expiry = SubscriptionManager.GetSubscriptionExpiry(CurrentUser.Email);
            confirmationLbl.Text = expiry.HasValue
                ? $"Subscription extended by {selectedDays} days.\nNew expiry: {expiry:dd-MM-yyyy}"
                : "Failed to update subscription.";
            confirmationLbl.Visible = true;

            if (this.Height < 500)
            {
                this.Height = 500;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));
                this.Invalidate();
                this.Update();
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