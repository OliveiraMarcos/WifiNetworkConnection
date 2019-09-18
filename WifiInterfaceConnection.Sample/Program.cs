using System;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.InterfaceConnection;

namespace WifiInterfaceConnection.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Listando redes
            var networkConn = new NetworkConnect();
            var allNetworks = await networkConn.NetworkWifi.GetNetworksAsync();
        }
    }
}
