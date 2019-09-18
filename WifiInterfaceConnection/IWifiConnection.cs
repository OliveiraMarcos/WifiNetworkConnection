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
        Task<bool> ConnectNetworkAsync(string SSID, string password);
        Task<IList<Network>> GetNetworksAsync();

    }
}
