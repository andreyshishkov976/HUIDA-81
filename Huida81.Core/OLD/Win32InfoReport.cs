using System.Collections.Generic;

namespace WMI
{
    public class Win32InfoReport
    {
        public Win32InfoReport(string infoCategory, List<Win32InfoGroup> infoGroups)
        {
            InfoCategory = infoCategory;
            InfoGroups = infoGroups;
        }

        public string InfoCategory { get; set; }
        public List<Win32InfoGroup> InfoGroups { get; set; }
    }
}
