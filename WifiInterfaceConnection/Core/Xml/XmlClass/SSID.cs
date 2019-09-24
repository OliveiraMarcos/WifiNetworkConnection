using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
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
}
