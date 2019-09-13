using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WifiInterfaceConnection.Core.InterfaceConnection;

namespace WifiInterfaceConnection
{
    public class NetworkConnect
    {
        public NetworkConnect()
        {
            NetworkWifi = Factory.Create();
        }
        public IWifiConnection NetworkWifi { get; set; }
    }
}
