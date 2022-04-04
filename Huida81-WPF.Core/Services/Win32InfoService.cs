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
        public Task<ICollection<Win32Info>> GetGridDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Win32Info>> GetListDetailsDataAsync(Win32InfoKey key)
        {
            await Task.CompletedTask;
            return await Task.Run(()=>GetWin32Info(key));
        }

        private ICollection<Win32Info> GetWin32Info(Win32InfoKey win32InfoKey)
        {
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher($@"SELECT * FROM {win32InfoKey}");
            List<Win32Info> infoGroups = new List<Win32Info>();
            foreach (ManagementObject hardwareObject in objectSearcher.Get())
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
