using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace VPN.Classes
{
    class Status
    {
        private static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static async Task<int> GetPingAsync(string input, bool isCountry = false)
        {
            string host = input;

            if (isCountry)
            {
                string configPath = Server.GetOpenVpnConfigPath(input);
                if (File.Exists(configPath))
                {
                    string hostLine = File.ReadAllLines(configPath)
                                          .FirstOrDefault(l => l.StartsWith("remote "));

                    if (!string.IsNullOrWhiteSpace(hostLine))
                    {
                        host = hostLine.Split(' ')[1];
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }

            try
            {
                long totalTime = 0;
                int echo = 4;
                int timeout = 999;
                Ping pingSender = new Ping();

                for (int i = 0; i < echo; i++)
                {
                    PingReply reply = await Task.Run(() => pingSender.Send(host, timeout));
                    if (reply.Status == IPStatus.Success)
                    {
                        totalTime += reply.RoundtripTime;
                    }
                }

                return (int)(totalTime / echo);
            }
            catch
            {
                return -1;
            }
        }


        static DateTime endSub = new DateTime(2026, 03, 03);
        public static int TotalSubDays()
        {
            TimeSpan subResult = endSub.Subtract(DateTime.Today);
            if (subResult.Days < 0)
                return 0;
            else
                return (int)subResult.TotalDays;
        }

        public static void AddSubscriptionDaysToCurrentUser(int days)
        {
            string email = CurrentUser.Email;
            DateTime now = DateTime.Today;

            var path = "subscriptions.db";
            var dict = File.Exists(path)
                ? File.ReadAllLines(path)
                      .Select(line => line.Split(':'))
                      .ToDictionary(parts => parts[0], parts => DateTime.Parse(parts[1]))
                : new Dictionary<string, DateTime>();

            if (dict.ContainsKey(email))
                dict[email] = dict[email] > now ? dict[email].AddDays(days) : now.AddDays(days);
            else
                dict[email] = now.AddDays(days);

            File.WriteAllLines(path, dict.Select(kv => $"{kv.Key}:{kv.Value:yyyy-MM-dd}"));
        }


        public static void ConnectionAuth()
        {
            while (!IsConnected())
            {
                var DialogResult = MessageBox.Show("You are not Connected to the internet", "No Connection",
                                                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (DialogResult == DialogResult.Cancel)
                {
                    break;
                }
            }

            if (!IsConnected())
            {
                Application.Exit();
            }
        }

        public static void AdministratorAuth()
        {
            if (!IsAdministrator())
            {
                var DialogResult = MessageBox.Show("Please Re-start as an Administrator", "Permission Denied",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (DialogResult == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
    }
}
