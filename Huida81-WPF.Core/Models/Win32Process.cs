using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Huida81_WPF.Core.Models
{
    public class Win32Process
    {
        public UInt64 KernelModeTime { get; set; }
        public string Name { get; set; }
        public UInt32 PageFileUsage { get; set; }
        public UInt32 ParentProcessId { get; set; }
        public UInt32 PeakPageFileUsage { get; set; }
        public UInt64 PeakVirtualSize { get; set; }
        public UInt32 PeakWorkingSetSize { get; set; }
        public UInt32 Priority { get; set; }
        public UInt64 PrivatePageCount { get; set; }
        public UInt32 ProcessId { get; set; }
        public UInt64 ReadOperationCount { get; set; }
        public UInt64 ReadTransferCount { get; set; }
        public UInt32 ThreadCount { get; set; }
        public UInt64 VirtualSize { get; set; }
        public string WindowsVersion { get; set; }
        public UInt64 WorkingSetSize { get; set; }

        public Win32Process(ManagementObject managementObject)
        {
            foreach (var prop in managementObject.Properties)
            {
                var processProp = GetType().GetProperty(prop.Name);
                if (processProp != null)
                {
                    processProp.SetValue(this, prop.Value);
                }
            }
        }
    }
}
