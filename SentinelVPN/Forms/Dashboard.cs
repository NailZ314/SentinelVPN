using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPN.Classes;
using VpnClient;
using VPN.Tunnels;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VPN
{
    public partial class Dashboard : Form
    {
        private const string DomainPlaceholder = "example.com or 192.168.0.1";
        private List<string> domainsAndIps = new List<string>();
        private readonly Size CompactWindowSize = new Size(417, 250);
        private readonly Size ExpandedWindowSize = new Size(417, 340);
        private VlessTunnelManager vlessManager = new VlessTunnelManager();
        private Tun2SocksManager tunManager = new Tun2SocksManager();

        public Dashboard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Resize += new System.EventHandler(this.Dashboard_Resize);
            DomainTxtBox.ForeColor = System.Drawing.Color.Gray;
            DomainTxtBox.Text = DomainPlaceholder;
            DomainTxtBox.GotFocus += RemovePlaceholderText;
            DomainTxtBox.LostFocus += ShowPlaceholderText;
            DomainTxtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DomainTxtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            DomainTxtBox.AutoCompleteCustomSource = LoadDomainHistory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VLESS_rBtn.Checked = true;
            Status.AdministratorAuth();
            Status.ConnectionAuth();
            CountriesCmBox.SelectedIndex = 0;
            DisconnectBtnEnabledFalse();
        }

        private async void CountriesCmBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountriesCmBox.SelectedItem == null)
                return;
            var ServerLocation = CountriesCmBox.SelectedItem.ToString();
            Vpn.ServerIp = Server.GetIp(ServerLocation);
            ImageLoader(ServerLocation);

            int pingResult = await Status.GetPingAsync(
               OpenVPN_rBtn.Checked ? ServerLocation : Vpn.ServerIp,
               isCountry: OpenVPN_rBtn.Checked
            );

            PingLbl.Text = pingResult > 0 ? $"{pingResult} ms" : "N/A";
            LastPingResult = pingResult;
        }

        private void UpdateCountriesByProtocol()
        {
            string protocol = PPTP_rBtn.Checked ? "PPTP" :
                              L2TP_rBtn.Checked ? "L2TP" :
                              OpenVPN_rBtn.Checked ? "OpenVPN" :
                              WireGuard_rBtn.Checked ? "WireGuard" :
                              VLESS_rBtn.Checked ? "VLESS" : "";

            CountriesCmBox.Items.Clear();
            CountriesCmBox.Items.AddRange(Server.GetCountriesByProtocol(protocol).ToArray());
            CountriesCmBox.SelectedIndex = 0;
        }

        private async void ConnectBtn_Click(object sender, EventArgs e)
        {
            int daysLeft = SubscriptionManager.GetRemainingDays(CurrentUser.Email);

            if (daysLeft <= 0)
            {
                MessageBox.Show("Your subscription has expired. Please renew to use the VPN.", "Subscription Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                using (var paymentForm = new PaymentForm())
                {
                    paymentForm.ShowDialog();
                }
                return;
            }

            if (CountriesCmBox.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Location", "Error at 0x40", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PPTP_rBtn.Checked && !L2TP_rBtn.Checked && !WireGuard_rBtn.Checked && !OpenVPN_rBtn.Checked && !VLESS_rBtn.Checked)
            {
                MessageBox.Show("Please Select a Protocol", "Error at 0x46", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckRequiredExecutables())
                return;

            try
            {
                if (PPTP_rBtn.Checked || L2TP_rBtn.Checked)
                {
                    Vpn.Connect();
                    await AnimateStatusChange($"Connected to {Vpn.VpnProtocol}", System.Drawing.Color.FromArgb(0, 179, 135));
                }
                else if (OpenVPN_rBtn.Checked)
                {
                    string selectedCountry = CountriesCmBox.SelectedItem?.ToString();
                    string configPath = Server.GetOpenVpnConfigPath(selectedCountry);
                    OpenVpnManager.Connect(configPath);
                    await AnimateStatusChange("Connected to OpenVPN", System.Drawing.Color.FromArgb(0, 179, 135));
                }
                else if (WireGuard_rBtn.Checked)
                {
                    string location = CountriesCmBox.SelectedItem.ToString();
                    WireGuardTunnelManager.UserPrivateKey = "aChy2Wye8IoGIkvg/C3Pe10lvz6jL4G1hWS+aMAIFUs=";

                    foreach (var entry in domainsAndIps)
                    {
                        RouteHelper.AddDomainIpsToCollection(entry);
                    }
                    if (WireGuardTunnelManager.Connect(location))
                    {
                        await AnimateStatusChange("Connected to WireGuard", System.Drawing.Color.FromArgb(0, 179, 135));
                    }
                    else
                    {
                        MessageBox.Show("WireGuard connection failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (VLESS_rBtn.Checked)
                {
                    string selectedLocation = CountriesCmBox.SelectedItem?.ToString();
                    vlessManager.ConnectVless();
                    tunManager.Start();
                    if (domainsAndIps.Count > 0)
                    {
                        foreach (var entry in domainsAndIps)
                        {
                            RouteHelper.AddRoutesForDomain(entry);
                        }
                    }
                    else
                    {
                        RouteHelper.AddFullRoute("10.66.66.1", "vpn", selectedLocation);
                    }
                    await AnimateStatusChange("Connected to VLESS", System.Drawing.Color.FromArgb(0, 179, 135));
                }
            }
            finally
            {
                ConnectBtnEnabledFalse();
                DisconnectBtnEnabledTrue();
                CountriesCmBoxEnabledFalse();
            }
        }

        private async void DisconnectBtn_Click(object sender, EventArgs e)
        {
            string selectedLocation = CountriesCmBox.SelectedItem?.ToString();
            try
            {
                if (PPTP_rBtn.Checked || L2TP_rBtn.Checked)
                {
                    Vpn.Disconnect();
                }
                else if (OpenVPN_rBtn.Checked)
                {
                    OpenVpnManager.Disconnect();
                }
                else if (WireGuard_rBtn.Checked)
                {
                    WireGuardTunnelManager.Disconnect();
                }
                else if (VLESS_rBtn.Checked)
                {
                    tunManager.Stop();
                    foreach (var proc in Process.GetProcessesByName("xray"))
                        proc.Kill();
                    vlessManager.DisconnectVless(selectedLocation);
                }
                await AnimateStatusChange("Disconnected", System.Drawing.Color.FromArgb(93, 97, 105));
            }
            finally
            {
                domainsAndIps.Clear();
                DomainsListBox.Items.Clear();
                ConnectBtnEnabledTrue();
                DisconnectBtnEnabledFalse();
                CountriesCmBoxEnabledTrue();
                DomainTxtBox.Text = DomainPlaceholder;
                DomainTxtBox.ForeColor = System.Drawing.Color.Gray;
            }
        }


        private void InfoBtn_Click(object sender, EventArgs e)
        {
            using (Information InfoDialog = new Information())
            {
               InfoDialog.ShowDialog();
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ExitMethod();
        }

        private void MiniBtn_Click(object sender, EventArgs e)
        {
            Hide();
            NotifyIcon.Visible = true;
            NotifyIcon.ContextMenuStrip = CMenu;
            NotifyIcon.ShowBalloonTip(1000);
        }

        private void openApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            NotifyIcon.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitMethod();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            NotifyIcon.Visible = false;
        }

        public void ExitMethod()
        {
            if (!ConnectBtn.Enabled)
            {
                MessageBox.Show("Please Disconnect First", "Error at 0x134", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Application.Exit();
            }
        }

        private void ShowSplitTunnelingOptions(bool show)
        {
            DomainTxtBox.Visible = show;
            AddDomainBtn.Visible = show;
            DomainsListBox.Visible = show;
            this.Size = show ? ExpandedWindowSize : CompactWindowSize;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));
            this.Invalidate();
            this.Update();
            StatusLbl.Top = show ? 315 : 220;
            StatusLbl.Left = (this.ClientSize.Width - StatusLbl.Width) / 2;
            StatusLbl.BringToFront();
        }

        private bool IsValidDomainOrIp(string input)
        {
            return System.Net.IPAddress.TryParse(input, out _) || Regex.IsMatch(input,
                                                               @"^([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$",
                                                               RegexOptions.IgnoreCase);
        }

        public static int LastPingResult { get; set; }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void statusPicbox_Click(object sender, EventArgs e)
        {

        }

        private void NameLbl_Click(object sender, EventArgs e)
        {

        }

        private void PPTP_rBtn_CheckedChanged(object sender, EventArgs e)
        {
            ShowSplitTunnelingOptions(false);
            if (PPTP_rBtn.Checked)
            {
                Vpn.VpnProtocol = "PPTP";
                UpdateCountriesByProtocol();
            }
        }

        private void L2TP_rBtn_CheckedChanged(object sender, EventArgs e)
        {
            ShowSplitTunnelingOptions(false);
            if (L2TP_rBtn.Checked)
            {
                Vpn.VpnProtocol = "L2TP";
                UpdateCountriesByProtocol();
            }
        }

        private void OpenVPN_rBtn_CheckedChanged(object sender, EventArgs e)
        {
            ShowSplitTunnelingOptions(false);
            if (OpenVPN_rBtn.Checked)
            {
                Vpn.VpnProtocol = "OpenVPN";
                UpdateCountriesByProtocol();
            }
        }

        private void WireGuard_rBtn_CheckedChanged(object sender, EventArgs e)
        {
            ShowSplitTunnelingOptions(WireGuard_rBtn.Checked);
            if (WireGuard_rBtn.Checked)
            {
                Vpn.VpnProtocol = "WireGuard";
                UpdateCountriesByProtocol();
            }
        }

        private void VLESS_rBtn_CheckedChanged(object sender, EventArgs e)
        {
            ShowSplitTunnelingOptions(VLESS_rBtn.Checked);
            if (VLESS_rBtn.Checked)
            {
                Vpn.VpnProtocol = "VLESS";
                UpdateCountriesByProtocol();
            }
        }

        private void CountriesFlgPicBox_Click(object sender, EventArgs e)
        {

        }

        private void AddDomainEntry()
        {
            string input = DomainTxtBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            if (!IsValidDomainOrIp(input))
            {
                MessageBox.Show("Invalid domain or IP address!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!domainsAndIps.Contains(input))
            {
                domainsAndIps.Add(input);
                DomainsListBox.Items.Add(input);
                SaveDomainToHistory(input);
                DomainTxtBox.Clear();
            }
        }

        private void AddDomainBtn_Click(object sender, EventArgs e)
        {
            AddDomainEntry();
        }

        private void DomainTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddDomainEntry();
                e.SuppressKeyPress = true;
            }
        }

        private void DomainsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (DomainsListBox.SelectedItem != null)
            {
                string selected = DomainsListBox.SelectedItem.ToString();
                domainsAndIps.Remove(selected);
                DomainsListBox.Items.Remove(selected);
            }
        }

        private void SaveDomainToHistory(string entry)
        {
            string historyDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SentinelVPN");
            string historyFile = Path.Combine(historyDir, "domain_history.txt");

            if (!Directory.Exists(historyDir))
                Directory.CreateDirectory(historyDir);

            var lines = File.Exists(historyFile) ? File.ReadAllLines(historyFile).ToList() : new List<string>();

            if (!lines.Contains(entry))
            {
                lines.Add(entry);
                File.WriteAllLines(historyFile, lines);
            }
        }

        private AutoCompleteStringCollection LoadDomainHistory()
        {
            var history = new AutoCompleteStringCollection();

            string historyDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SentinelVPN");
            string historyFile = Path.Combine(historyDir, "domain_history.txt");

            if (!Directory.Exists(historyDir))
                Directory.CreateDirectory(historyDir);

            if (File.Exists(historyFile))
            {
                var lines = File.ReadAllLines(historyFile);
                history.AddRange(lines);
            }

            return history;
        }

        private async Task AnimateStatusChange(string text, System.Drawing.Color color)
        {
            for (int i = 100; i >= 0; i -= 20)
            {
                StatusLbl.ForeColor = System.Drawing.Color.FromArgb(i * color.A / 100, color);
                await Task.Delay(10);
            }

            StatusLbl.Text = text;
            StatusLbl.ForeColor = System.Drawing.Color.FromArgb(0, color);

            StatusLbl.Top = DomainTxtBox.Visible ? 315 : 220;
            StatusLbl.Left = (this.ClientSize.Width - StatusLbl.Width) / 2;
            StatusLbl.BringToFront();

            for (int i = 0; i <= 100; i += 20)
            {
                StatusLbl.ForeColor = System.Drawing.Color.FromArgb(i * color.A / 100, color);
                await Task.Delay(10);
            }
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            StatusLbl.Left = (this.ClientSize.Width - StatusLbl.Width) / 2;
        }

        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            if (DomainTxtBox.Text == DomainPlaceholder)
            {
                DomainTxtBox.Text = "";
                DomainTxtBox.ForeColor = System.Drawing.Color.FromArgb(0, 179, 135);
            }
        }

        private void ShowPlaceholderText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DomainTxtBox.Text))
            {
                DomainTxtBox.Text = DomainPlaceholder;
                DomainTxtBox.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private bool CheckRequiredExecutables()
        {
            var errors = new List<string>();

            if (WireGuard_rBtn.Checked && !File.Exists(WireGuardTunnelManager.ExecutablePath))
                errors.Add("wireguard.exe");

            if (OpenVPN_rBtn.Checked && !File.Exists(OpenVpnManager.ExecutablePath))
                errors.Add("openvpn.exe");

            if (VLESS_rBtn.Checked)
            {
                if (!File.Exists(VlessTunnelManager.ExecutablePath))
                    errors.Add("xray.exe");

                if (!File.Exists(Tun2SocksManager.ExecutablePath))
                    errors.Add("tun2socks.exe");
            }

            if (errors.Count > 0)
            {
                MessageBox.Show("Missing required file(s):\n" + string.Join("\n", errors) + "\nPlease, install required applications.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Graphics g = e.Graphics)
            using (System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(20, 255, 255, 255), 1))
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

        private void StatusLbl_Click(object sender, EventArgs e)
        {

        }
    }
}