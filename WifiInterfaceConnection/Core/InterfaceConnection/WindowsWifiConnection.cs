using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public class WindowsWifiConnection : IWifiConnection
    {
        #region Private
        private const string _fileName = "netsh";
        private const string _argumentsListNetwork = "wlan show networks";
        private readonly Process NewProcess = new Process();

        public Task<bool> IsConnected { get; set; }

        private Task<string[]> GetAllNetworksAsync()
        {
            var task = new Task<string[]>(() => 
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
            });
            task.Start();
            return task;
        }

        #endregion

        #region Public
        
        public Task<bool> ConnectNetworkAsync(string SSID, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Network>> GetNetworksAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
