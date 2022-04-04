﻿using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Huida81.Core.Models
{
    public class Win32Process
    {
        public ulong KernelModeTime { get; set; }
        public string Name { get; set; }
        public uint PageFileUsage { get; set; }
        public uint ParentProcessId { get; set; }
        public uint PeakPageFileUsage { get; set; }
        public ulong PeakVirtualSize { get; set; }
        public uint PeakWorkingSetSize { get; set; }
        public uint Priority { get; set; }
        public ulong PrivatePageCount { get; set; }
        public uint ProcessId { get; set; }
        public ulong ReadOperationCount { get; set; }
        public ulong ReadTransferCount { get; set; }
        public uint ThreadCount { get; set; }
        public ulong VirtualSize { get; set; }
        public string WindowsVersion { get; set; }
        public ulong WorkingSetSize { get; set; }

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
