using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
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
}
