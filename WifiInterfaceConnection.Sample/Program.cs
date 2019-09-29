using System;
using System.Linq;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Adapter;
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
            var myNetwork = allNetworks.Where(e => e.SSID == "MySSID").FirstOrDefault();
            networkConn.NetworkWifi.ConnectNetworkAsync(myNetwork,"MyPassword");
            if (networkConn.NetworkWifi.IsConnected.Result)
            {
                Console.WriteLine("Conected");
            }
            else
            {
                foreach (var err in networkConn.NetworkWifi.GetErrors())
                {
                    Console.WriteLine(err);
                }
            }
        }
    }
}
