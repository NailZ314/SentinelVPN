using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.ServiceProcess;

namespace VPN.Classes
{
    public static class OpenVpnManager
    {
        private static Process _vpnProcess;
        private static string _credFilePath;
        private static readonly string OpenVpnExe;

        public static string ExecutablePath => OpenVpnExe;

        static OpenVpnManager()
        {
            OpenVpnExe = LocateOpenVpnExe();
        }

        public static void Connect(string configPath)
        {
            if (!File.Exists(configPath))
            {
                MessageBox.Show("OpenVPN config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _credFilePath = Path.Combine(Path.GetTempPath(), "openvpn_auth.txt");
            File.WriteAllLines(_credFilePath, new string[] { "vpnbook", "huba7re" });

            var psi = new ProcessStartInfo
            {
                FileName = OpenVpnExe,
                Arguments = $"--config \"{configPath}\" --auth-user-pass \"{_credFilePath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            _vpnProcess = new Process { StartInfo = psi };
            _vpnProcess.Start();

            Task.Run(() =>
            {
                while (!_vpnProcess.StandardOutput.EndOfStream)
                {
                    string line = _vpnProcess.StandardOutput.ReadLine();
                    Console.WriteLine("[OpenVPN] " + line);
                }
            });
        }

        public static void Disconnect()
        {
            try
            {
                if (_vpnProcess != null && !_vpnProcess.HasExited)
                {
                    _vpnProcess.Kill();
                    _vpnProcess.WaitForExit(3000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to kill OpenVPN: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _vpnProcess?.Dispose();
                _vpnProcess = null;

                if (File.Exists(_credFilePath))
                {
                    try { File.Delete(_credFilePath); } catch { }
                }
            }
        }

        private static string LocateOpenVpnExe()
        {
            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "OpenVPN", "bin", "openvpn.exe"
            );

            if (File.Exists(defaultPath))
                return defaultPath;

            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
            foreach (var dir in paths)
            {
                try
                {
                    string fullPath = Path.Combine(dir.Trim(), "openvpn.exe");
                    if (File.Exists(fullPath))
                        return fullPath;
                }
                catch { }
            }

            return null;
        }
    }
}

