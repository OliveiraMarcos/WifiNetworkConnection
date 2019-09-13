using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public class WindowsWifiConnection : IWifiConnection
    {
        #region Private
        private const string _fileName = "netsh";
        private const string _argumentsListNetwork = "wlan show networks";
        private readonly Process NewProcess = new Process();

        private string[] GetAllNetworks()
        {
            NewProcess.StartInfo.FileName = _fileName;
            NewProcess.StartInfo.Arguments = _argumentsListNetwork;

            try
            {
                using (Process execute = Process.Start(NewProcess.StartInfo))
                {
                    return execute.StandardOutput.ReadToEnd().Split("\r\n".ToCharArray());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Public
        public bool ConnectNetwork(string SSID, string password)
        {
            throw new NotImplementedException();
        }

        public IList<Network> GetNetworks()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
