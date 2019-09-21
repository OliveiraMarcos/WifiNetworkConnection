using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection
{
    public interface IWifiConnection
    {
        List<string> GetErrors();
        Task<bool> IsConnected { get; set; }
        Task<bool> ConnectNetworkTaskAsync(Network network, string password);
        Task<bool> ConnectNetworkTaskAsync(string SSID, string password);
        void ConnectNetworkAsync(Network network, string password);
        void ConnectNetworkAsync(string SSID, string password);
        Task<IList<Network>> GetNetworksAsync();

    }
}
