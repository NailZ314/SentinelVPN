using System.Net;
using System.Linq;
using DotRas;

namespace VPN.Classes
{
    static class Vpn
    {
        public static string ServerIp = string.Empty;
        public static string VpnProtocol = "PPTP";

        private static string AdapterName => VpnProtocol == "L2TP" ? "VPN_L2TP" : "VPN_PPTP";
        private static string PPTP_Username = "vpnbook";
        private static string PPTP_Password = "huba7re";
        private static string L2TP_Username = "vpn";
        private static string L2TP_Password = "vpn";
        private static string L2TP_PSK = "vpn";

        private static RasDialer _dialer;
        private static RasHandle _handle;

        public static void Connect()
    {
        _dialer = new RasDialer();
        string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

        using (RasPhoneBook PhoneBook = new RasPhoneBook())
        {
            PhoneBook.Open(phoneBookPath);

            if (PhoneBook.Entries.Contains(AdapterName))
            {
                PhoneBook.Entries.Remove(AdapterName);
            }

            RasEntry entry = null;
            var devices = RasDevice.GetDevices();
            RasDevice selectedDevice = null;

            if (VpnProtocol == "PPTP")
            {
                selectedDevice = devices.FirstOrDefault(d => d.DeviceType == RasDeviceType.Vpn && d.Name.Contains("PPTP"));
                if (selectedDevice != null)
                {
                    entry = RasEntry.CreateVpnEntry(
                        AdapterName,
                        ServerIp,
                        RasVpnStrategy.PptpOnly,
                        selectedDevice
                    );
                }
            }
            else if (VpnProtocol == "L2TP")
            {
                selectedDevice = devices.FirstOrDefault(d => d.DeviceType == RasDeviceType.Vpn && d.Name.Contains("L2TP"));
                if (selectedDevice != null)
                {
                    entry = RasEntry.CreateVpnEntry(
                        AdapterName,
                        ServerIp,
                        RasVpnStrategy.L2tpOnly,
                        selectedDevice
                    );
                    entry.Options.UsePreSharedKey = true;
                    entry.Options.RequireDataEncryption = true;
                    PhoneBook.Entries.Add(entry);
                    entry.UpdateCredentials(RasPreSharedKey.Client, L2TP_PSK);
                }
            }

            if (entry != null && VpnProtocol == "PPTP")
            {
                PhoneBook.Entries.Add(entry);
            }
        }

        _dialer.EntryName = AdapterName;
        _dialer.PhoneBookPath = phoneBookPath;

        if (VpnProtocol == "PPTP")
        {
            _dialer.Credentials = new NetworkCredential(PPTP_Username, PPTP_Password);
        }
        else if (VpnProtocol == "L2TP")
        {
            _dialer.Credentials = new NetworkCredential(L2TP_Username, L2TP_Password);
        }

        _handle = _dialer.DialAsync();
    }


    public static void Disconnect()
        {
            if (_dialer?.IsBusy == true)
            {
                _dialer.DialAsyncCancel();
            }
            else if (_handle != null)
            {
                var connections = RasConnection.GetActiveConnections();
                var connection = connections.FirstOrDefault(c => c.Handle == _handle);
                connection?.HangUp();
            }

            using (RasPhoneBook PhoneBook = new RasPhoneBook())
            {
                PhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));

                if (PhoneBook.Entries.Contains(AdapterName))
                {
                    PhoneBook.Entries.Remove(AdapterName);
                }
            }
        }
    }
}