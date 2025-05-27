using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System;

namespace VPN.Classes
{
    static class Server
    {
        private static readonly Dictionary<string, string> _pptpServers = new Dictionary<string, string>()
        {
            { "Stockholm (SE)", "77.239.97.41" },
            { "Warsaw 1 (PL)", "PL134.vpnbook.com" },
            { "Warsaw 2 (PL)", "PL140.vpnbook.com" },
            { "Roubaix 1 (FR)", "FR200.vpnbook.com" },
            { "Roubaix 2 (FR)", "FR231.vpnbook.com" },
            { "Frankfurt 1 (DE)", "DE20.vpnbook.com" },
            { "Frankfurt 2 (DE)", "DE220.vpnbook.com" },
            { "London 1 (UK)", "UK205.vpnbook.com" },
            { "London 2 (UK)", "UK68.vpnbook.com" },
            { "Reston 1 (US)", "US16.vpnbook.com" },
            { "Reston 2 (US)", "US178.vpnbook.com" },
            { "Montréal 1 (CA)", "CA149.vpnbook.com" },
            { "Montréal 2 (CA)", "CA196.vpnbook.com" }
        };

        private static readonly Dictionary<string, string> _l2tpServers = new Dictionary<string, string>()
        {
            { "Stockholm (SE)", "77.239.97.41" },
            { "Frankfurt (DE)", "3.71.20.205" },
            { "London (UK)", "18.130.101.93" },
            { "São Paulo (BR)", "219.100.37.192" },
            { "Tokyo (JP)", "219.100.37.196" }
        };

        private static readonly Dictionary<string, string> _openVpnServers = new Dictionary<string, string>
        {
            { "Stockholm (SE)", "sweden.ovpn" },
            { "Frankfurt (DE)", "germany.ovpn" },
            { "London 1 (UK)", "uk1.ovpn" },
            { "London 2 (UK)", "uk.ovpn" },
            { "Warsaw (PL)", "poland.ovpn" },
            { "Roubaix (FR)", "france.ovpn" },
            { "Reston (US)", "us.ovpn" },
            { "Montréal (CA)", "canada.ovpn" }
        };

        private static readonly Dictionary<string, string> _wireGuardServers = new Dictionary<string, string>()
        {
            { "Stockholm (SE)", "77.239.97.41" },
            { "Frankfurt (DE)", "3.71.20.205" },
            { "London (UK)", "18.130.101.93" }
        };

        private static readonly Dictionary<string, string> _vlessServers = new Dictionary<string, string>()
        {
            { "Stockholm (SE)", "nailz314.duckdns.org" }
        };

        public class WireGuardEntry
        {
            public string PublicKey { get; set; }
            public int Port { get; set; }
        }

        private static readonly Dictionary<string, WireGuardEntry> _wireGuardKeys;

        static Server()
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "wireguard_keys.json");
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                _wireGuardKeys = JsonConvert.DeserializeObject<Dictionary<string, WireGuardEntry>>(json);
            }
            else
            {
                _wireGuardKeys = new Dictionary<string, WireGuardEntry>();
            }
        }

        public static string GetOpenVpnConfigPath(string location)
        {
            if (_openVpnServers.TryGetValue(location, out var configFile))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", configFile);

            }
            return null;
        }

        public static string GetWireGuardEndpoint(string location)
        {
            if (_wireGuardServers.ContainsKey(location) && _wireGuardKeys.ContainsKey(location))
            {
                string ip = _wireGuardServers[location];
                int port = _wireGuardKeys[location].Port;
                return $"{ip}:{port}";
            }
            return string.Empty;
        }


        public static Dictionary<string, string> GetServers(string protocol)
        {
            switch (protocol)
            {
                case "PPTP":
                    return _pptpServers;
                case "L2TP":
                    return _l2tpServers;
                case "OpenVPN":
                    return _openVpnServers;
                case "WireGuard":
                    return _wireGuardServers;
                case "VLESS":
                    return _vlessServers;
                default:
                    return new Dictionary<string, string>();
            }
        }

        public static string GetIp(string location)
        {
            var servers = GetServers(Vpn.VpnProtocol);
            return servers.ContainsKey(location) ? servers[location] : string.Empty;
        }

        public static string GetWireGuardPublicKey(string location)
        {
            return _wireGuardKeys.ContainsKey(location) ? _wireGuardKeys[location].PublicKey : string.Empty;
        }

        public static List<string> GetCountriesByProtocol(string protocol)
        {
            switch (protocol)
            {
                case "PPTP":
                    return _pptpServers.Keys.ToList();
                case "L2TP":
                    return _l2tpServers.Keys.ToList();
                case "OpenVPN":
                    return _openVpnServers.Keys.ToList();
                case "WireGuard":
                    return _wireGuardServers.Keys.ToList();
                case "VLESS":
                    return _vlessServers.Keys.ToList();
                default:
                    return new List<string>();
            }
        }
    }
}
