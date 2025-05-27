using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using VPN.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Linq;
using System.Net.NetworkInformation;

namespace VPN
{
    public partial class Information : Form
    {
        
        private ChartValues<double> downloadValues = new ChartValues<double>();
        private ChartValues<double> uploadValues = new ChartValues<double>();
        private List<string> labels = new List<string>();
        public LiveCharts.WinForms.CartesianChart speedChart;
        private SpeedTester _tester;
        private CancellationTokenSource _cts;
        private readonly Size CompactWindowSize = new Size(420, 270);
        private readonly Size ExpandedWindowSize = new Size(420, 470);

        public Information()
        {
            InitializeComponent();
            InitializeSpeedChart();
        }

        private void Information_Load(object sender, EventArgs e)
        {
            int daysLeft = SubscriptionManager.GetRemainingDays(CurrentUser.Email);
            SubLblDyn.Text = $"Active Subscription: {daysLeft} Days Left";
            SubLblDyn.Left = (this.ClientSize.Width - SubLblDyn.Width) / 2;
            if (daysLeft <= 0)
            {
                MessageBox.Show("Your subscription has expired. Please renew to continue using the VPN.", "Subscription Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                using (var paymentForm = new PaymentForm())
                {
                    paymentForm.ShowDialog();
                }
                return;
            }
            FetchAndDisplayPublicIp();
            UpdateConnectionInfo();
            StopSpeedTestBtnEnabledFalse();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MiniBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            string rememberFile = "remember.dat";

            if (File.Exists(rememberFile))
            {
                File.Delete(rememberFile);
            }

            MessageBox.Show("You have been logged out successfully. The application will now close.", "Logout");
            Application.Exit();
        }

        private void RenewBtn_Click(object sender, EventArgs e)
        {
            using (var paymentForm = new PaymentForm())
            {
                paymentForm.ShowDialog();
            }
        }

        private void InitializeSpeedChart()
        {
            speedChart = new LiveCharts.WinForms.CartesianChart
            {
                Location = new System.Drawing.Point(10, 260),
                Size = new System.Drawing.Size(380, 200),
                Visible = false,
                BackColor = System.Drawing.Color.FromArgb(35, 39, 43)
            };

            speedChart.Series = new SeriesCollection

            {
                new LineSeries
                {
                    Title = "Download Mbps",
                    Values = downloadValues,
                    StrokeThickness = 3,
                    LineSmoothness = 0.3,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 0, 179, 135)),
                    Stroke = System.Windows.Media.Brushes.LimeGreen,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 6
                },
                new LineSeries
                {
                    Title = "Upload Mbps",
                    Values = uploadValues,
                    StrokeThickness = 3,
                    LineSmoothness = 0.3,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 135, 135, 255)),
                    Stroke = System.Windows.Media.Brushes.DeepSkyBlue,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 6
                }
            };

            speedChart.AxisX.Add(new Axis
            {
                Title = "Time (s)",
                Labels = labels,
                Separator = new Separator
                {
                    StrokeThickness = 0,
                    Stroke = System.Windows.Media.Brushes.Gray
                },
                Foreground = System.Windows.Media.Brushes.LightGray
            });

            speedChart.AxisY.Add(new Axis
            {
                Title = "Speed (Mbps)",
                MinValue = 0,
                MaxValue = 100,
                Separator = new Separator { StrokeThickness = 1, Stroke = System.Windows.Media.Brushes.Gray },
                Foreground = System.Windows.Media.Brushes.LightGray
            });

            this.Controls.Add(speedChart);
        }

        private async void SpeedTestBtn_Click(object sender, EventArgs e)
        {
            StartSpeedTestBtnEnabledFalse();
            StopSpeedTestBtnEnabledTrue();
            UpdateConnectionInfo();
            ShowSpeedTestChart(true);
            _cts = new CancellationTokenSource();
            _tester = new SpeedTester();
            downloadValues.Clear();
            uploadValues.Clear();
            labels.Clear();
            speedChart.AxisX[0].Labels.Clear();

            _tester.OnDownloadMeasured += (mbps) =>
            {
                if (mbps <= 0) return;
                this.Invoke(new Action(() =>
                {
                    downloadValues.Add(mbps);
                    labels.Add(DateTime.Now.ToString("HH:mm:ss"));

                    if (downloadValues.Count > 30)
                    {
                        downloadValues.RemoveAt(0);
                        labels.RemoveAt(0);
                    }

                    speedChart.AxisX[0].Labels.Clear();
                    foreach (var label in labels)
                        speedChart.AxisX[0].Labels.Add(label);

                    if (mbps > speedChart.AxisY[0].MaxValue * 0.9)
                        speedChart.AxisY[0].MaxValue = mbps * 1.2;

                    speedChart.Update();
                }));
            };

            _tester.OnUploadMeasured += (mbps) =>
            {
                this.Invoke(new Action(() =>
                {
                    uploadValues.Add(mbps);
                    if (uploadValues.Count > 30)
                        uploadValues.RemoveAt(0);
                    speedChart.Update();
                }));
            };

            await Task.Run(() => _tester.StartTestAsync(_cts.Token));
        }

        private void StopSpeedTestBtn_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
            StopSpeedTestBtnEnabledFalse();
            StartSpeedTestBtnEnabledTrue();
        }

        private void ShowSpeedTestChart(bool show)
        {
            speedChart.Visible = show;
            this.Size = show ? ExpandedWindowSize : CompactWindowSize;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));
            this.Invalidate();
            this.Update();
        }

        private async void FetchAndDisplayPublicIp()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string ip = await client.GetStringAsync("https://icanhazip.com");
                    IpLabel.Text = "IP: " + ip.Trim();
                }
            }
            catch
            {
                IpLabel.Text = "Unable to fetch IP";
            }

            IpLabel.Visible = true;
        }

        private string GetTunnelIP()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.GetIPProperties().UnicastAddresses.Any(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                );

            foreach (var ni in interfaces)
            {
                if (ni.Name.ToLower().Contains("vpn") || ni.Name.ToLower().Contains("tun") || ni.Name.ToLower().Contains("tap") || ni.Name.ToLower().Contains("wg"))
                {
                    var ip = ni.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    if (ip != null)
                        return $"{ip.Address} [{ni.Name}]";
                }
            }

            return "VPN Not Connected";
        }

        private string GetDnsServers()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.GetIPProperties().UnicastAddresses.Any(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                );

            foreach (var ni in interfaces)
            {
                var dnsList = ni.GetIPProperties().DnsAddresses;
                var ipv4Dns = dnsList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList();

                if (ipv4Dns.Count > 0)
                {
                    return string.Join(", ", ipv4Dns);
                }
            }

            return "No DNS Servers Found";
        }

        private bool IsVpnInterfaceActive()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Any(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.GetIPProperties().UnicastAddresses.Any(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) &&
                    (
                        ni.Name.ToLower().Contains("vpn") ||
                        ni.Name.ToLower().Contains("tun") ||
                        ni.Name.ToLower().Contains("tap") ||
                        ni.Name.ToLower().Contains("wg")
                    )
                );
        }

        private void UpdateConnectionInfo()
        {
            TunIpLabel.Text = "Tunnel IP: " + GetTunnelIP();
            DnsLabel.Text = "DNS: " + GetDnsServers();

            if (IsVpnInterfaceActive())
            {
                LatencyLabel.Text = Dashboard.LastPingResult > 0
                    ? $"Latency: {Dashboard.LastPingResult} ms"
                    : "Latency: N/A";
            }
            else
            {
                LatencyLabel.Text = "Latency: VPN Not Connected";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Information_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
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

        private void NameLbl_Click(object sender, EventArgs e) { }
        private void SubLblStatic_Click(object sender, EventArgs e) { }
    }
}
