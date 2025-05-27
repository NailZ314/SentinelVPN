using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using VPN.Classes;

namespace VpnClient
{
    public class VlessTunnelManager
    {
        private Process xrayProcess;
        private static readonly string XrayExe;

        public static string ExecutablePath => XrayExe;

        static VlessTunnelManager()
        {
            XrayExe = LocateXrayExe();
        }

        public void ConnectVless()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string clientConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "vless.json");

            if (!File.Exists(clientConfig))
            {
                MessageBox.Show("vless.json not found!\nExpected at:\n" + clientConfig, "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            xrayProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = XrayExe,
                    Arguments = $"run -config \"{clientConfig}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            xrayProcess.Start();
        }

        public void DisconnectVless(string selectedLocation)
        {
            if (xrayProcess != null && !xrayProcess.HasExited)
            {
                xrayProcess.Kill();
                xrayProcess.Dispose();

            }

            try
            {
                string serverTarget = Server.GetIp(selectedLocation);

                if (!string.IsNullOrEmpty(serverTarget) && !IPAddress.TryParse(serverTarget, out _))
                {
                    var resolved = Dns.GetHostAddresses(serverTarget)
                                      .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    if (resolved != null)
                        serverTarget = resolved.ToString();
                }

                if (!string.IsNullOrEmpty(serverTarget))
                {
                    var deleteRoute = Process.Start(new ProcessStartInfo
                    {
                        FileName = "route",
                        Arguments = $"DELETE {serverTarget} MASK 255.255.255.255",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
                    deleteRoute.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete route: " + ex.Message);
            }
        }

        private static string LocateXrayExe()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string exeInApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xray", "xray.exe");

            if (File.Exists(exeInApp))
                return exeInApp;

            string[] paths = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? new string[0];
            foreach (string dir in paths)
            {
                try
                {
                    string candidate = Path.Combine(dir.Trim(), "xray.exe");
                    if (File.Exists(candidate))
                        return candidate;
                }
                catch { }
            }

            return null;
        }
    }
}