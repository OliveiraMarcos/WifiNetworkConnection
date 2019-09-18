using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public sealed class OSXWifiConnection : IWifiConnection
    {
        public Task<bool> IsConnected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<bool> ConnectNetworkAsync(string SSID, string password)
        {
            throw new NotImplementedException();
        }

        public List<string> GetErrors()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Network>> GetNetworksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
