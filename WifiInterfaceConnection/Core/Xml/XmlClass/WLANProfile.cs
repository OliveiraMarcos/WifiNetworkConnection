using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WifiInterfaceConnection.Core.Model;
using WifiInterfaceConnection.Core.Xml.XmlEnum;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
    [XmlRoot(Namespace = "http://www.microsoft.com/networking/WLAN/profile/v1")]
    public class WLANProfile : FileXml
    {
        protected WLANProfile()
        {

        }
        public WLANProfile(string passwd, Network network, string connectionMode = "manual")
        {
            Name = network.SSID;
            ConnectionMode = connectionMode;
            ConnectionType = ConnectionTypeEnum.ESS;
            SSIDConfig = new List<SSID>() { new SSID(network.SSID) };
            MSM = new List<Security>() { new Security(new AuthEncryption(AuthenticationTypeEnum.WPAPSK, EncryptionTypeEnum.AES), new SharedKey(passwd)) };
            MacRandomization = new MacRandomization();
        }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        public List<SSID> SSIDConfig { get; set; }
        [XmlElement(ElementName = "connectionType")]
        public ConnectionTypeEnum ConnectionType { get; set; }
        [XmlElement(ElementName = "connectionMode")]
        public string ConnectionMode { get; set; }
        [XmlArrayItem("security")]
        public List<Security> MSM { get; set; }
        [XmlElement(Namespace = "http://www.microsoft.com/networking/WLAN/profile/v3")]
        public MacRandomization MacRandomization { get; set; }

    }

    public class Factory
    {
        public static WLANProfile New(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(WLANProfile));
                return serializer.Deserialize(stringReader) as WLANProfile;
            }
        }
    }

}
