using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareManager
{
    public class HardwareProp
    {
        public HardwareProp(string propertyName, string propertyValue)
        {
            Name = propertyName;
            Value = propertyValue;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
