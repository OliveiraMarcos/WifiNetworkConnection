using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Xml.XmlEnum;

namespace WifiInterfaceConnection.Core.Model
{
    public class Network
    {
        public string SSID { get; set; }
        public string NetworkType { get; set; }
        public string Authentication { get; set; }
        public string Encryption { get; set; }
    }
}
