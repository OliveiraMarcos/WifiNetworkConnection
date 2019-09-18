using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Model;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public sealed class WindowsWifiConnection : IWifiConnection
    {
        #region Private
        private const string _fileName = "netsh";
        private const string _argumentsListNetwork = "wlan show networks";
        private readonly Process NewProcess = new Process();
        private List<string> Erros { get; set; }

        public Task<bool> IsConnected { get; set; }

        private Task<Dictionary<int, Dictionary<int, string>>> GetAllNetworksAsync()
        {
            var task = new Task<Dictionary<int, Dictionary<int, string>>>(() => 
            { 
                NewProcess.StartInfo.FileName = _fileName;
                NewProcess.StartInfo.Arguments = _argumentsListNetwork;
                NewProcess.StartInfo.RedirectStandardOutput = true;
                try
                {
                    using (Process execute = Process.Start(NewProcess.StartInfo))
                    {
                        var arrNets = execute.StandardOutput.ReadToEnd().Split("\r\n".ToCharArray());
                        var listDic = new Dictionary<int, Dictionary<int, string>>();
                        Dictionary<int, string> dicTemp = null;
                        var group = 0;
                        var ct = 0;
                        foreach (var line in arrNets)
                        {
                            if (line.Contains("SSID"))
                            {
                                group++;
                                dicTemp = new Dictionary<int, string>();
                            }
                            if (line.Contains(":") && group > 0)
                            {
                                var item = line.Split(new char[] { ':' });
                                dicTemp.Add(ct, item[1]);
                                ct++;
                            }
                            if (ct > 3)
                            {
                                listDic.Add(group, dicTemp);
                                ct = 0;
                            }
                        }
                        return listDic;
                    }
                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                    throw;
                }
            });
            task.Start();
            return task;
        }

        #endregion

        #region Public

        public WindowsWifiConnection()
        {
            Erros = new List<string>();
        }
        
        public Task<bool> ConnectNetworkAsync(string SSID, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Network>> GetNetworksAsync()
        {
            var dicNetworks = await GetAllNetworksAsync();
            var result = new List<Network>();
            foreach (var value in dicNetworks.Values)
            {
                result.Add(new Network() { SSID = value[0].Trim(),
                                           NetworkType = value[1].Trim(),
                                           Authentication = value[2].Trim(),
                                           Encryption = value[3].Trim()
                });
            }
            return result;
        }

        public List<string> GetErrors()
        {
            return Erros;
        }
        #endregion
    }
}
