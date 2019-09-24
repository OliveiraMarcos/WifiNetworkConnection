using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Attributes;

namespace WifiInterfaceConnection.Core.Xml.XmlEnum
{
    public enum AuthenticationTypeEnum
    {
        [AuthenticationType("Open 802.11 authentication")]
        open,
        [AuthenticationType("Shared 802.11 authentication")]
        shared,
        [AuthenticationType("WPA-Enterprise 802.11 authentication")]
        WPA,
        [AuthenticationType("WPA-Personal 802.11 authentication")]
        WPAPSK,
        [AuthenticationType("WPA2-Enterprise 802.11 authentication")]
        WPA2,
        [AuthenticationType("WPA2-Personal 802.11 authentication")]
        WPA2PSK
    }
}
