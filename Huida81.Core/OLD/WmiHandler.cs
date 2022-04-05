using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace WMI
{
    public class WmiHandler
    {
        public List<string> HardwareKeys = new List<string>()
        {
            "Win32_Processor",
            "Win32_VideoController",
            "Win32_IDEController",
            "Win32_Battery",
            "Win32_BIOS",
            "Win32_PhysicalMemory",
            "Win32_CacheMemory",
            "Win32_USBController",
            "Win32_DiskDrive",
            "Win32_LogicalDisk",
            "Win32_Keyboard",
            "Win32_NetworkAdapter",
            "Win32_Account",
            "Win32_BaseBoard",
            "Win32_Process",
            "Win32_DesktopMonitor",
            "Win32_PointingDevice",
        };

        public List<Win32InfoGroup> GetWin32InfoReportByKey(string hardwareKey)
        {
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher($@"SELECT * FROM {hardwareKey}");
            List<Win32InfoGroup> infoGroups = new List<Win32InfoGroup>();
            foreach (ManagementObject hardwareObject in objectSearcher.Get())
            {
                var infoGroup = new Win32InfoGroup(hardwareObject["Name"].ToString());

                foreach (var data in hardwareObject.Properties)
                {
                    if (data.Value != null && data.Value != string.Empty)
                    {
                        infoGroup.InfoProps.Add(new Win32InfoProp(data.Name, data.Value.ToString()));
                    }
                }
                infoGroups.Add(infoGroup);
            }
            return infoGroups;
        }

        public void GetWin32InfoReportJson(string savePath)
        {
            List<Win32InfoReport> infoReports = new List<Win32InfoReport>();
            foreach (var key in HardwareKeys)
            {
                infoReports.Add(new Win32InfoReport(key, GetWin32InfoReportByKey(key)));
            }
            string json = JsonConvert.SerializeObject(infoReports, Formatting.Indented);
            StreamWriter writer = new StreamWriter(savePath, false);
            writer.WriteAsync(json);
        }
    }
}
