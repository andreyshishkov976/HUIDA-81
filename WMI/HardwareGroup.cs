using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareManager
{
    public class HardwareGroup
    {
        public HardwareGroup(string group)
        {
            Group = group;
            HardwareProps = new List<HardwareProp>();
        }

        public string Group { get; set; }

        public List<HardwareProp> HardwareProps { get; set; }
    }
}
