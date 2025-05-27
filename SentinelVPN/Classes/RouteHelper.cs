using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace VPN.Classes
{
    public static class RouteHelper
    {
        private static List<string> collectedIps = new List<string>();

        public static void AddDomainIpsToCollection(string domain)
        {
            try
            {
                var allIps = Dns.GetHostAddresses(domain);
                var ipv4Ips = allIps
                    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(ip => ip.ToString());

                collectedIps.AddRange(ipv4Ips);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to resolve domain '{domain}': {ex.Message}");
            }
        }

        public static List<string> GetCollectedIps()
        {
            return collectedIps.Count > 0 ? collectedIps.Distinct().ToList()
                                          : new List<string> { "0.0.0.0/0" };
        }

        public static void ClearCollectedIps()
        {
            collectedIps.Clear();
        }

        public static void AddRoutesForDomain(string domain, string gateway = "10.66.66.1", string interfaceName = "vpn")
        {
            try
            {
                var allIps = Dns.GetHostAddresses(domain);
                var ipv4Ips = allIps.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

                if (ipv4Ips.Length == 0)
                {
                    MessageBox.Show($"Domain '{domain}' resolved, but no IPv4 addresses found.\nResolved IPs: {string.Join(", ", allIps.Select(ip => ip.ToString()))}");
                    return;
                }

                int ifIndex = -1;
                for (int i = 0; i < 10; i++)
                {
                    ifIndex = GetInterfaceIndexByName(interfaceName);
                    if (ifIndex != -1)
                        break;
                    Thread.Sleep(500);
                }

                if (ifIndex == -1)
                {
                    MessageBox.Show($"Interface '{interfaceName}' not found.");
                    return;
                }

                foreach (var ip in ipv4Ips)
                {
                    var proc = Process.Start(new ProcessStartInfo
                    {
                        FileName = "route",
                        Arguments = $"ADD {ip} MASK 255.255.255.255 {gateway} IF {ifIndex}",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    proc.WaitForExit();
                    if (proc.ExitCode != 0)
                    {
                        Debug.WriteLine($"Route add for {ip} failed (exit {proc.ExitCode}).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Route add failed: {ex.Message}");
            }
        }

        public static void AddFullRoute(string gateway, string interfaceName, string selectedLocation)
        {
            try
            {
                int ifIndex = GetInterfaceIndexByName(interfaceName);
                if (ifIndex == -1)
                {
                    MessageBox.Show($"Interface '{interfaceName}' not found.");
                    return;
                }

                string serverTarget = Server.GetIp(selectedLocation);

                if (!IPAddress.TryParse(serverTarget, out _))
                {
                    var resolved = Dns.GetHostAddresses(serverTarget)
                                      .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    if (resolved == null)
                    {
                        MessageBox.Show($"Failed to resolve domain: {serverTarget}");
                        return;
                    }
                    serverTarget = resolved.ToString();
                }

                var bypassProxy = Process.Start(new ProcessStartInfo
                {
                    FileName = "route",
                    Arguments = $"ADD {serverTarget} MASK 255.255.255.255 192.168.0.1",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                bypassProxy.WaitForExit();

                var defaultRoute = Process.Start(new ProcessStartInfo
                {
                    FileName = "route",
                    Arguments = $"ADD 0.0.0.0 MASK 0.0.0.0 {gateway} METRIC 10 IF {ifIndex}",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                defaultRoute.WaitForExit();

                if (defaultRoute.ExitCode != 0)
                {
                    Debug.WriteLine($"Default route add failed (exit {defaultRoute.ExitCode}).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add full route: {ex.Message}");
            }
        }

        private static int GetInterfaceIndexByName(string name)
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.Name.Contains(name))
                .Select(ni => ni.GetIPProperties().GetIPv4Properties()?.Index ?? -1)
                .FirstOrDefault(index => index != -1);
        }
    }
}
