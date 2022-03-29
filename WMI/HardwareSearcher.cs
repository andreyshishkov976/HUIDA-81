using System;
using System.Collections.Generic;
using System.Management;

namespace HardwareManager
{
    public static class HardwareSearcher
    {
        #region Nested Types
        public enum HardwareKeys
        {
            Win32_Processor,
            Win32_VideoController,
            Win32_IDEController,
            Win32_Battery,
            Win32_BIOS,
            Win32_PhysicalMemory,
            Win32_CacheMemory,
            Win32_USBController,
            Win32_DiskDrive,
            Win32_LogicalDisk,
            Win32_Keyboard,
            Win32_NetworkAdapter,
            Win32_Account
        }
        #endregion Nested Types

        public static List<HardwareGroup> GetHardwareInfo(HardwareKeys hardwareKey)
        {
            ManagementObjectSearcher objectSearcher = new($@"SELECT * FROM {hardwareKey}");

            try
            {
                List<HardwareGroup> hardwareReport = new();
                foreach (ManagementObject hardwareObject in objectSearcher.Get())
                {
                    var report = new HardwareGroup(hardwareObject["Name"].ToString());
                    
                    foreach (var data in hardwareObject.Properties)
                    {
                        if (data.Value != null)
                        {
                            report.HardwareProps.Add(new HardwareProp(data.Name, data.Value.ToString()));
                        }
                        else
                        {
                            report.HardwareProps.Add(new HardwareProp(data.Name, "null"));
                        }
                    }
                    hardwareReport.Add(report);
                }
                return hardwareReport;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
