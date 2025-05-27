# SentinelVPN: VPN Client with Selective Traffic Routing

## Description

**SentinelVPN** is a Windows VPN client designed to optimize remote employee connectivity by enabling secure and flexible access to corporate resources. Unlike traditional VPN clients, SentinelVPN supports selective traffic routing, allowing users to choose which applications, domains, or ports use the VPN tunnel and which bypass it. This ensures efficient use of network resources, better performance, and high data security.

---

## Features

* **Multiple Protocol Support:** PPTP, L2TP/IPsec, OpenVPN, WireGuard, VLESS (Xray-core).
* **Selective Traffic Routing:**

  * Route traffic per application, port, domain, or IP address.
  * Flexible configuration for individual or corporate use.
* **Modern UI:**

  * Dark theme, green accents, intuitive controls.
* **Diagnostics Panel:**

  * Real-time display of VPN status, IP, DNS, latency, and speed tests.
* **Single Sign-On:** Integration with corporate databases for unified authentication.
* **Security:**

  * Strong encryption protocols (AES, ChaCha20, TLS).
* **Extensible architecture:** Easy addition of new protocols and features.

---

## System Requirements

* **OS:** Windows 10/11 x64
* **RAM:** 2 GB or higher
* **Disk Space:** 200 MB free
* **Processor:** Intel Core i3 or higher
* **.NET Framework:** 4.8 or higher

---

## Installation

1. **Download** the latest SentinelVPN release from [releases page](#) or clone this repository.
2. **Extract** the archive to your preferred folder.
3. **Install** required dependencies:

   * .NET Framework 4.8
   * [DotRas](https://github.com/winnster/DotRas)
   * [OpenVPN](https://openvpn.net/community-downloads/)
   * [Xray-core](https://github.com/XTLS/Xray-core)
   * [WireGuard Tools](https://www.wireguard.com/install/)
4. **Run** `SentinelVPN.exe` as Administrator.
5. **Configure** your VPN server details and selective routing options in the application interface.

---

## Usage

1. **Login/Register** with your corporate or personal account.
2. **Select** the desired VPN protocol and server.
3. **Configure** selective routing:

   * Choose applications from the running process list.
   * Specify ports or domain names for routing.
4. **Connect** to the VPN. The app will automatically manage traffic according to your selections.
5. **Monitor** your connection via the Information Panel (speed, ping, IP, DNS).
6. **Disconnect** securely when finished.

---

## Configuration

* **Servers:** Add/remove servers in the `Servers` tab.
* **Selective Routing:** Use dropdowns or input fields to specify processes, domains, or ports.
* **Diagnostics:** View connection logs and statistics in the Information panel.

---

## Contributors

* Yersultan Kenzhetayev (Lead Developer)
* Aslan Abishev (Developer)
* Ernur Amalbek (Contributor)

---

## Acknowledgments

* DotRas
* Xray-core & VLESS
* WireGuard Project
* OpenVPN Community