using System;
using System.Collections.Generic;
using System.Text;

namespace WifiInterfaceConnection.Core.Attributes
{
    public class AuthenticationTypeAttribute:Attribute
    {
        public string Description { get; set; }
        public AuthenticationTypeAttribute(string description)
        {
            Description = description;
        }
    }
}
