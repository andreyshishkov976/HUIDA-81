using Huida81_WPF.Core.Contracts.Services;
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
        public Task<ICollection<Win32CpuInfo>> GetGridDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Win32CpuInfo>> GetListDetailsDataAsync()
        {
            await Task.CompletedTask;
            return GetWin32Info();
        }

        private ICollection<Win32CpuInfo> GetWin32Info()
        {
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher($@"SELECT * FROM Win32_Processor");
            ICollection<Win32CpuInfo> infoGroups = new List<Win32CpuInfo>();
            foreach (ManagementObject managementObject in objectSearcher.Get())
            {
                infoGroups.Add(new Win32CpuInfo(managementObject));
            }
            return infoGroups;
        }
    }
}
