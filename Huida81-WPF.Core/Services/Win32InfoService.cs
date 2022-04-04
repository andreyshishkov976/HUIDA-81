using Huida81_WPF.Core.Contracts.Services;
using Huida81_WPF.Core.Enums;
using Huida81_WPF.Core.Models;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Huida81_WPF.Core.Services
{
    public class Win32InfoService : IWin32InfoService
    {
        public ManagementObjectSearcher _objectSearcher = new ManagementObjectSearcher();

        public async Task<ICollection<Win32Process>> GetWin32ProcessDataAsync()
        {
            await Task.CompletedTask;
            return await Task.Run(() => GetWin32Processes());

        }

        private ICollection<Win32Process> GetWin32Processes()
        {
            _objectSearcher.Query.QueryString = $@"SELECT * FROM Win32_Process";
            ICollection<Win32Process> win32Processes = new List<Win32Process>();
            foreach (ManagementObject hardwareObject in _objectSearcher.Get())
            {
                win32Processes.Add(new Win32Process(hardwareObject));
            }
            return win32Processes;
        }

        public async Task<ICollection<Win32Info>> GetWin32InfoDataAsync(Win32InfoKey key)
        {
            await Task.CompletedTask;
            return await Task.Run(()=>GetWin32Infos(key));
        }

        private ICollection<Win32Info> GetWin32Infos(Win32InfoKey win32InfoKey)
        {
            _objectSearcher.Query.QueryString = $@"SELECT * FROM {win32InfoKey}";
            ICollection<Win32Info> infoGroups = new List<Win32Info>();
            foreach (ManagementObject hardwareObject in _objectSearcher.Get())
            {
                var infoGroup = new Win32Info(hardwareObject["Name"].ToString(), hardwareObject["Description"].ToString());

                foreach (var data in hardwareObject.Properties)
                {
                    if (data.Value != null && data.Value.ToString() != string.Empty)
                    {
                        infoGroup.InfoDetails.Add(new Win32InfoDetail(data.Name, data.Value.ToString()));
                    }
                }
                infoGroups.Add(infoGroup);
            }
            return infoGroups;
        }
    }
}
