using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
    public class SharedKey
    {
        protected SharedKey()
        {

        }
        public SharedKey(string keyMaterial, string keyType = "passPhrase", bool _protected = false)
        {
            KeyType = keyType;
            KeyMaterial = keyMaterial;
            Protected = _protected;
        }
        [XmlElement(ElementName = "keyType")]
        public string KeyType { get; set; }
        [XmlElement(ElementName = "protected")]
        public bool Protected { get; set; }
        [XmlElement(ElementName = "keyMaterial")]
        public string KeyMaterial { get; set; }
    }
}
