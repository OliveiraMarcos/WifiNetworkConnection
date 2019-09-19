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
            var teste = new WLANProfile("SSID","senhadd", allNetworks.FirstOrDefault());
            var test = teste.ToXML();
            //var xml = new Xml();
            //xml.SSIDConfig.SSID.hex = "jbljbb";
            //xml.SSIDConfig.SSID.name = "ssid";
            //xml.name = "ssid";

            //var teste = xml.ToXML();
        }
    }
}
