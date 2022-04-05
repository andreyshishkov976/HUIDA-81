using Huida81.Core.Contracts.Services;
using Huida81.Core.Enums;
using Huida81.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace Huida81.Core.Services
{
    public class Win32InfoService : IWin32InfoService
    {
        private ManagementObjectSearcher _objectSearcher = new ManagementObjectSearcher();
        private IFileService _fileService = new FileService();

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
            return await Task.Run(() => GetWin32Infos(key));
        }

        private ICollection<Win32Info> GetWin32Infos(Win32InfoKey win32InfoKey)
        {
            _objectSearcher.Query.QueryString = $@"SELECT * FROM {win32InfoKey}";
            ICollection<Win32Info> win32Infos = new List<Win32Info>();
            foreach (ManagementObject managementObject in _objectSearcher.Get())
            {
                var infoGroup = new Win32Info(managementObject["Name"].ToString(), managementObject["Description"].ToString());

                foreach (var data in managementObject.Properties)
                {
                    if (data.Value != null && data.Value.ToString() != string.Empty)
                    {
                        infoGroup.InfoDetails.Add(new Win32InfoDetail(data.Name, data.Value.ToString()));
                    }
                }
                win32Infos.Add(infoGroup);
            }
            return win32Infos;
        }

        public async Task SaveWin32InfoDataAsync(string folderPath, string fileName)
        {
            await Task.CompletedTask;
            string report = string.Empty;
            List<Win32Info> win32Infos = new List<Win32Info>();
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_BaseBoard)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_BIOS)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_Processor)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_DiskDrive)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_LogicalDisk)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_VideoController)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_DesktopMonitor)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_Keyboard)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_PointingDevice)));
            win32Infos.AddRange(await Task.Run(() => GetWin32Infos(Win32InfoKey.Win32_NetworkAdapter)));
            foreach (var info in win32Infos)
            {
                report += info.ToString();
            }
            var processes = await Task.Run(() => GetWin32Processes());
            report += "\r\nProcesses:\r\n";
            foreach (var process in processes)
            {
                report += process.ToString();
            }
            FileInfo fileInfo = new FileInfo(fileName);
            await Task.Run(() => _fileService.Save(fileInfo.DirectoryName, fileInfo.Name, report));
        }
    }
}
