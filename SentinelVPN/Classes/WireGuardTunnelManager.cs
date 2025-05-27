using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using VPN.Classes;

namespace VPN.Tunnels
{
    public class WireGuardTunnelManager
    {
        private const string ConfigDir = "config";
        private static readonly string WireGuardExe;
        private const string TunnelName = "wgclient";
        public static string UserPrivateKey = "aChy2Wye8IoGIkvg/C3Pe10lvz6jL4G1hWS+aMAIFUs=";

        public static string ExecutablePath => WireGuardExe;

        static WireGuardTunnelManager()
        {
            WireGuardExe = LocateWireGuardExe();
        }

        public static bool Connect(string location)
        {
            try
            {
                Directory.CreateDirectory(ConfigDir);

                string publicKey = Server.GetWireGuardPublicKey(location);
                string endpoint = Server.GetWireGuardEndpoint(location);
                var allowedIps = RouteHelper.GetCollectedIps();
                string allowedIpsJoined = string.Join(", ", allowedIps);

                if (string.IsNullOrEmpty(publicKey) || string.IsNullOrEmpty(endpoint))
                    throw new Exception("Missing WireGuard server details.");

                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "template.conf");

                if (!File.Exists(templatePath))
                {
                    System.Windows.Forms.MessageBox.Show("Template file not found at:\n" + templatePath);
                    return false;
                }

                string template = File.ReadAllText(templatePath);
                string filledConf = template
                    .Replace("{{PRIVATE_KEY}}", UserPrivateKey)
                    .Replace("{{PUBLIC_KEY}}", publicKey)
                    .Replace("{{ENDPOINT}}", endpoint)
                    .Replace("{{ALLOWED_IPS}}", allowedIpsJoined);

                string confPath = Path.Combine(ConfigDir, $"{TunnelName}.conf");
                File.WriteAllText(confPath, filledConf);

                if (!File.Exists(confPath))
                {
                    System.Windows.Forms.MessageBox.Show("Failed to create WireGuard config file at:\n" + confPath);
                    return false;
                }

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = WireGuardExe,
                        Arguments = $"/installtunnelservice \"{Path.GetFullPath(confPath)}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };
                process.Start();

                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    System.Windows.Forms.MessageBox.Show("WireGuard failed to start.\n\n" +
                        "STDOUT:\n" + stdout + "\n\nSTDERR:\n" + stderr);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("WireGuard connection error:\n" + ex.Message);
                return false;
            }
        }

        public static void Disconnect()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = WireGuardExe,
                    Arguments = "/uninstalltunnelservice wgclient",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            process.Start();

            process.WaitForExit();

            RouteHelper.ClearCollectedIps();

            string confPath = Path.Combine(ConfigDir, $"{TunnelName}.conf");

            if (File.Exists(confPath))
                File.Delete(confPath);
        }

        private static string FindWireGuardInPath()
        {
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
            foreach (var dir in paths)
            {
                try
                {
                    string fullPath = Path.Combine(dir.Trim(), "wireguard.exe");
                    if (File.Exists(fullPath))
                        return fullPath;
                }
                catch { }
            }

            return null;
        }

        private static string LocateWireGuardExe()
        {
            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "WireGuard", "wireguard.exe"
            );

            if (File.Exists(defaultPath))
                return defaultPath;

            return FindWireGuardInPath();
        }
    }
}