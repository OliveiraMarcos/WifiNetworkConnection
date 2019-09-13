using System;
using System.Collections.Generic;
using System.Text;
using WifiInterfaceConnection.Core.Adapter;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public class Factory
    {
        public static IWifiConnection Create()
        {
            switch (SOAdapter.GetOSPlatform())
            {
                case OSPlatformEnum.Windows:
                    return new WindowsWifiConnection();
                case OSPlatformEnum.MacOS:
                    return new OSXWifiConnection();
                case OSPlatformEnum.Linux:
                    return new LinuxWifiConnection();
                case OSPlatformEnum.NotDefined:
                default: throw new PlatformNotSupportedException("This platform is not supported!");
            }
        }
    }
}
