using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Huida81_WPF.Core.Models
{
    public class Win32CpuInfo : Win32InfoBase
    {
        public UInt32 CurrentClockSpeed { get; set; }
        public UInt32 CurrentVoltage { get; set; }
        public string DeviceID { get; set; }
        public UInt32 ExtClock { get; set; }
        public UInt16 Family { get; set; }
        public UInt32 L2CacheSize { get; set; }
        public UInt32 L2CacheSpeed { get; set; }
        public UInt32 L3CacheSize { get; set; }
        public UInt32 L3CacheSpeed { get; set; }
        public string Manufacturer { get; set; }
        public UInt32 MaxClockSpeed { get; set; }
        public UInt32 NumberOfCores { get; set; }
        public UInt32 NumberOfEnabledCore { get; set; }
        public UInt32 NumberOfLogicalProcessors { get; set; }
        public string ProcessorId { get; set; }
        public UInt16 Revision { get; set; }
        public string Role { get; set; }
        public string SerialNumber { get; set; }
        public string SocketDesignation { get; set; }
        public string Status { get; set; }
        public string Stepping { get; set; }
        public string SystemName { get; set; }
        public UInt32 ThreadCount { get; set; }
        public string Version { get; set; }
        public bool VirtualizationFirmwareEnabled { get; set; }
        public bool VMMonitorModeExtensions { get; set; }

        public Win32CpuInfo(ManagementObject managementObject) : base(managementObject)
        {
        }
    }
}
