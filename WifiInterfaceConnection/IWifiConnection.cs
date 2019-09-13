using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection
{
    public interface IWifiConnection
    {
        bool ConnectNetwork(string SSID, string password);
        IList<Network> GetNetworks();
    }
}
