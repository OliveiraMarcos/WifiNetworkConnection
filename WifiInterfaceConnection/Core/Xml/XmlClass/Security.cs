using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
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
}
