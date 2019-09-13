using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public class LinuxWifiConnection : IWifiConnection
    {
        public bool ConnectNetwork(string SSID, string password)
        {
            throw new NotImplementedException();
        }

        public IList<Network> GetNetworks()
        {
            throw new NotImplementedException();
        }
    }
}
