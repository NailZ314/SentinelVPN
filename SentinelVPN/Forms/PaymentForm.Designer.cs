using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static VPN.LoginForm;

namespace VPN
{
    public partial class PaymentForm : Form
    {
        private System.Windows.Forms.Button ExitBtn, MiniBtn;
        private System.Windows.Forms.Label titleLbl;

        private void InitializeComponent()
        {
            this.Text = "Payment";
            this.Size = new Size(380, 455);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(35, 39, 43);
            this.ForeColor = Color.White;
            this.Font = new Font("Verdana", 8.25F);
            this.Icon = new Icon(new MemoryStream(SentinelVPN.Properties.Resources.app_icon));

            titleLbl = new Label()
            {
                Text = "Payment",
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

            Label SeprLbl = new Label()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Size = new Size(this.Width - 8, 1),
                Location = new Point(4, 40)
            };

            cardLbl = new Label() { Text = "Card Number", Top = 50, Left = 20, Width = 120, ForeColor = Color.Gray };
            cardNumberTxt = new TextBox() { Top = 75, Left = 20, Width = 340, MaxLength = 19, BackColor = Color.FromArgb(35, 39, 43), ForeColor = Color.FromArgb(225, 227, 232), BorderStyle = BorderStyle.FixedSingle };
            cardNumberTxt.TextChanged += CardNumberTxt_TextChanged;
            cardNumberTxt.KeyPress += OnlyDigits_KeyPress;

            expiryLbl = new Label() { Text = "Expiry (MM/YY)", Top = 115, Left = 20, Width = 120, ForeColor = Color.Gray };
            expiryTxt = new TextBox() { Top = 140, Left = 20, Width = 100, MaxLength = 5, BackColor = Color.FromArgb(35, 39, 43), ForeColor = Color.FromArgb(225, 227, 232), BorderStyle = BorderStyle.FixedSingle };
            expiryTxt.TextChanged += ExpiryTxt_TextChanged;
            expiryTxt.KeyPress += OnlyDigitsAndSlash_KeyPress;

            cvvLbl = new Label() { Text = "CVV", Top = 115, Left = 140, Width = 50, ForeColor = Color.Gray };
            cvvTxt = new TextBox() { Top = 140, Left = 140, Width = 60, MaxLength = 3, BackColor = Color.FromArgb(35, 39, 43), ForeColor = Color.FromArgb(225, 227, 232), BorderStyle = BorderStyle.FixedSingle };
            cvvTxt.TextChanged += (s, e) => {
                cvvTxt.Text = new string(cvvTxt.Text.Where(char.IsDigit).ToArray());
                cvvTxt.SelectionStart = cvvTxt.Text.Length;
            };
            cvvTxt.KeyPress += OnlyDigits_KeyPress;

            durationButtons = new Button[durations.Length];
            priceLabels = new Label[prices.Length];
            for (int i = 0; i < durations.Length; i++)
            {
                int yOffset = 185 + i * 40;
                int days = durations[i];
                int price = prices[i];

                durationButtons[i] = new Button()
                {
                    Text = $"Buy {days} days",
                    Top = yOffset,
                    Left = 20,
                    Width = 150,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(35, 39, 43),
                    ForeColor = Color.FromArgb(0, 179, 135)
                };
                durationButtons[i].FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);

                priceLabels[i] = new Label()
                {
                    Text = $"{price} ₸",
                    Top = yOffset + 7,
                    Left = 180,
                    Width = 100,
                    ForeColor = Color.FromArgb(0, 179, 135)
                };

                int capturedDays = days;
                durationButtons[i].Click += (s, e) => selectedDays = capturedDays;

                this.Controls.Add(durationButtons[i]);
                this.Controls.Add(priceLabels[i]);
            }

            Button confirmBtn = new Button()
            {
                Text = "Confirm Payment",
                Top = 390,
                Left = 20,
                Width = 340,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(0, 179, 135),
                BackColor = Color.FromArgb(35, 39, 43)
            };
            confirmBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 179, 135);
            confirmBtn.Click += ConfirmBtn_Click;

            confirmationLbl = new Label()
            {
                Top = 450,
                Left = 20,
                Width = 350,
                Height = 40,
                ForeColor = Color.Green,
                Font = new Font("Verdana", 9, FontStyle.Bold),
                Visible = false
            };

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
            this.Controls.Add(cardLbl);
            this.Controls.Add(cardNumberTxt);
            this.Controls.Add(expiryLbl);
            this.Controls.Add(expiryTxt);
            this.Controls.Add(cvvLbl);
            this.Controls.Add(cvvTxt);
            this.Controls.Add(confirmBtn);
            this.Controls.Add(confirmationLbl);
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
