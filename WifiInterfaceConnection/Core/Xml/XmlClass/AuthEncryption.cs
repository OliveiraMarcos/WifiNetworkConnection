using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WifiInterfaceConnection.Core.Xml.XmlEnum;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
    public class AuthEncryption
    {
        protected AuthEncryption()
        {

        }
        public AuthEncryption(AuthenticationTypeEnum authentication, EncryptionTypeEnum encryption, bool useOneX = false)
        {
            Authentication = authentication;
            Encryption = encryption;
            UseOneX = useOneX;
        }
        [XmlElement(ElementName = "authentication")]
        public AuthenticationTypeEnum Authentication { get; set; }
        [XmlElement(ElementName = "encryption")]
        public EncryptionTypeEnum Encryption { get; set; }
        [XmlElement(ElementName = "useOneX")]
        public bool UseOneX { get; set; }
    }
}
