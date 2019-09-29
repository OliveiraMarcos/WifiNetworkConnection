using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiInterfaceConnection.Core.Adapter;
using WifiInterfaceConnection.Core.Extension;
using WifiInterfaceConnection.Core.Model;
using WifiInterfaceConnection.Core.Xml.XmlClass;

namespace WifiInterfaceConnection.Core.InterfaceConnection
{
    public sealed class WindowsWifiConnection : IWifiConnection
    {
        #region Private
        private const string _fileName = "netsh";
        private const string _argumentsListNetwork = "wlan show networks";
        private const string _argumentsFileName = "wlan add profile filename=\"{0}\"";
        private const string _argumentsConnect = "wlan connect  ssid={0} name={0}";
        private const string _argumentsInterfaces = "wlan show interfaces";
        public WLANProfile ProfileXml { get; private set; }
        private readonly Process NewProcess = new Process();
        private List<string> Erros { get; set; }

        public Task<bool> IsConnected { get; set; }
        private async Task<bool> AddProfile()
        {
            var fileNeme = ProfileXml.GetFullName();
            NewProcess.StartInfo.Arguments = string.Format(_argumentsFileName,fileNeme);
            try
            {
                using (Process execute = Process.Start(NewProcess.StartInfo))
                {
                    await execute.WaitForExitAsync();
                    var arrNets = execute.StandardOutput.ReadToEnd().Split("\r\n".ToCharArray());
                    foreach (var item in arrNets)
                    {
                        if(!string.IsNullOrEmpty(item))
                            Erros.Add(item);
                    }
                    return await ConnectProfile();
                }
            }
            catch (Exception ex)
            {
                Erros.Add(ex.Message);
                throw;
            }
        }

        private async Task<bool> ConnectProfile()
        {
            NewProcess.StartInfo.Arguments = string.Format(_argumentsConnect,ProfileXml.Name);
            try
            {
                using (Process execute = Process.Start(NewProcess.StartInfo))
                {
                    await execute.WaitForExitAsync();
                    var arrNets = execute.StandardOutput.ReadToEnd().Split("\r\n".ToCharArray());
                    foreach (var item in arrNets)
                    {
                        if(!string.IsNullOrEmpty(item))
                            Erros.Add(item);
                    }
                    return await ShowConnectionAsync();
                }
            }
            catch (Exception ex)
            {
                Erros.Add(ex.Message);
                throw;
            }
        }



        private async Task<Dictionary<int, Dictionary<int, string>>> GetAllNetworksAsync()
        {
            NewProcess.StartInfo.Arguments = _argumentsListNetwork;
            try
            {
                using (Process execute = Process.Start(NewProcess.StartInfo))
                {
                    await execute.WaitForExitAsync();
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
        }

        private async Task<bool> ShowConnectionAsync()
        {
            NewProcess.StartInfo.Arguments = _argumentsInterfaces;
            try
            {
                using (Process execute = Process.Start(NewProcess.StartInfo))
                {
                    await execute.WaitForExitAsync();
                    var arrNets = execute.StandardOutput.ReadToEnd().Split("\r\n".ToCharArray());
                    foreach (var item in arrNets)
                    {
                        if (item.Contains("SSID"))
                        {
                            var line = item.Split(new char[] { ':' });
                            if (line[1].Trim().Equals(ProfileXml.Name))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Public

        public WindowsWifiConnection()
        {
            NewProcess.StartInfo.UseShellExecute = false;
            NewProcess.StartInfo.CreateNoWindow = true;
            NewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            NewProcess.StartInfo.RedirectStandardOutput = true;
            NewProcess.StartInfo.FileName = _fileName;
            Erros = new List<string>();
        }
        public void ConnectNetworkAsync(string SSID, string password)
        {
            IsConnected = ConnectNetworkTaskAsync(SSID, password);
        }

        public void ConnectNetworkAsync(Network network, string password)
        {
             IsConnected = ConnectNetworkTaskAsync(network, password);
        }
        public async Task<bool> ConnectNetworkTaskAsync(string SSID, string password)
        {

            try
            {
                var listAll = await GetNetworksAsync();
                var selectedNet = listAll.Where(e => e.SSID == SSID).FirstOrDefault();
                ProfileXml = new WLANProfile(password, selectedNet);
                ProfileXml.SaveAsync();
                return await AddProfile();
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ConnectNetworkTaskAsync(Network network, string password)
        {
            try
            {
                ProfileXml = new WLANProfile(password, network);
                ProfileXml.SaveAsync();
                return await AddProfile();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<Network>> GetNetworksAsync()
        {
            var dicNetworks = await GetAllNetworksAsync();
            var result = new List<Network>();
            foreach (var value in dicNetworks.Values)
            {
                result.Add(new Network()
                {
                    SSID = value[0].Trim(),
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
