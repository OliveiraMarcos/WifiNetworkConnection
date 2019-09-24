using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Xml.XmlEnum;

namespace WifiInterfaceConnection.Core.Model
{
    public class Network
    {
        public string SSID { get; set; }
        public ConnectionTypeEnum NetworkType { get; set; }
        public AuthenticationTypeEnum Authentication { get; set; }
        public EncryptionTypeEnum Encryption { get; set; }
    }
}
