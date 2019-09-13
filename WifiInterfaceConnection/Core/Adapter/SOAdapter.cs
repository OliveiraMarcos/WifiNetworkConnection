using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.Adapter
{
    public class SOAdapter
    {
        public static OSPlatformEnum GetOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatformEnum.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatformEnum.MacOS;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatformEnum.Linux;
            }

            return OSPlatformEnum.NotDefined;
        }
    }
}
