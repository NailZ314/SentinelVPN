using System;
using System.Activities.Statements;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace VPN.Tunnels
{
    public class Tun2SocksManager
    {
        private Process tun2socksProcess;
        private static readonly string Tun2SocksExe;
        private const string adapterName = "vpn";

        public static string ExecutablePath => Tun2SocksExe;

        static Tun2SocksManager()
        {
            Tun2SocksExe = LocateTun2SocksExe();
        }

        public void Start()
        {
            string tun2socksConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "tun2socks.yaml");

            if (!File.Exists(tun2socksConfig))
            {
                MessageBox.Show("tun2socks.yaml not found!", "Missing Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tun2socksProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Tun2SocksExe,
                    Arguments = $"-config \"{tun2socksConfig}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            tun2socksProcess.Start();

            RunPowerShell($"New-NetIPAddress -InterfaceAlias '{adapterName}' -IPAddress 10.66.66.2 -PrefixLength 24");
            RunPowerShell($"Set-DnsClientServerAddress -InterfaceAlias '{adapterName}' -ServerAddresses 1.1.1.1,1.0.0.1");
        }

        public void Stop()
        {
            try
            {
                if (tun2socksProcess != null && !tun2socksProcess.HasExited)
                {
                    tun2socksProcess.Kill();
                    tun2socksProcess.Dispose();
                    tun2socksProcess = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping tun2socks: " + ex.Message);
            }
        }

        private static string LocateTun2SocksExe()
        {
            string exeInApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xray", "tun2socks.exe");
            if (File.Exists(exeInApp))
                return exeInApp;

            string[] paths = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? new string[0];
            foreach (string dir in paths)
            {
                try
                {
                    string candidate = Path.Combine(dir.Trim(), "tun2socks.exe");
                    if (File.Exists(candidate))
                        return candidate;
                }
                catch { }
            }

            return null;
        }

        private void RunPowerShell(string command)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-Command \"{command}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = psi })
            {
                process.Start();
                process.WaitForExit();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show("[PowerShell Error] " + error);
            }
        }
    }
}
