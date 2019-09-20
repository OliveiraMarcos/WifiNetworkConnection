using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.Adapter
{
    public class WLANProfile
    {
        public string ToXML()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stringwriter, this);
                return stringwriter.ToString();
            }
        }
        public async Task SaveAsync(string pathName)
        {
            await File.WriteAllTextAsync(pathName, ToXML());
    }
        protected WLANProfile()
        {

        }
        public WLANProfile(string ssid, string passwd, Network network, string connectionMode = "manual")
        {
            Name = ssid;
            ConnectionMode = connectionMode;
            ConnectionType = network.NetworkType;
            SSIDConfig = new List<SSID>() {new SSID(ssid) };
            Security = new Security(new AuthEncryption(network.Authentication, network.Encryption), new SharedKey(passwd));
        }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        public List<SSID> SSIDConfig { get; set; }
        [XmlElement(ElementName = "connectionType")]
        public string ConnectionType { get; set; }
        [XmlElement(ElementName = "connectionMode")]
        public string ConnectionMode { get; set; }
        [XmlElement(ElementName = "security")]
        public Security Security { get; set; }
        public MacRandomization MacRandomization { get; set; }

    }
    
    public class SSID
    {
        protected SSID()
        {

        }
        public SSID(string name)
        {
            Hex = BitConverter.ToString(Encoding.Default.GetBytes(name)).Replace("-", "");
            Name = name;
        }
        [XmlElement(ElementName = "hex")]
        public string Hex { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }
    public class Security
    {
        protected Security()
        {

        }
        public Security(AuthEncryption authEncryption, SharedKey sharedKey)
        {
            AuthEncryption = authEncryption;
            SharedKey = sharedKey;
        }
        [XmlElement(ElementName = "authEncryption")]
        public AuthEncryption AuthEncryption { get; set; }
        [XmlElement(ElementName = "sharedKey")]
        public SharedKey SharedKey { get; set; }
    }

    public class AuthEncryption
    {
        protected AuthEncryption()
        {

        }
        public AuthEncryption(string authentication, string encryption, bool useOneX = false)
        {
            Authentication = authentication;
            Encryption = encryption;
            UseOneX = useOneX;
        }
        [XmlElement(ElementName = "authentication")]
        public string Authentication { get; set; }
        [XmlElement(ElementName = "encryption")]
        public string Encryption { get; set; }
        [XmlElement(ElementName = "useOneX")]
        public bool UseOneX { get; set; }
    }
    public class SharedKey
    {
        protected SharedKey()
        {

        }
        public SharedKey(string keyMaterial, string keyType = "passPhrase",  bool _protected = false)
        {
            KeyType = keyType;
            KeyMaterial = KeyMaterial;
            Protected = _protected;
        }
        [XmlElement(ElementName = "keyType")]
        public string KeyType { get; set; }
        [XmlElement(ElementName = "protected")]
        public bool Protected { get; set; }
        [XmlElement(ElementName = "keyMaterial")]
        public string KeyMaterial { get; set; }
    }
    public class MacRandomization
    {
        protected MacRandomization()
        {

        }
        public MacRandomization(bool enableRandomization = false)
        {
            EnableRandomization = enableRandomization;
        }
        [XmlElement(ElementName = "enableRandomization")]
        public bool EnableRandomization { get; set; }
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
