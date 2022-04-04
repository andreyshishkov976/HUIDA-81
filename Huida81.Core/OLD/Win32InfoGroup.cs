using System.Collections.Generic;

namespace WMI
{
    public class Win32InfoGroup
    {
        public Win32InfoGroup(string group)
        {
            Group = group;
            InfoProps = new List<Win32InfoProp>();
        }

        public string Group { get; set; }

        public List<Win32InfoProp> InfoProps { get; set; }
    }
}
