using System;
using System.Collections.Generic;
using System.Text;

namespace Huida81_WPF.Core.Models
{
    public class Win32InfoDetail
    {
        public Win32InfoDetail(string propertyName, string propertyValue)
        {
            Name = propertyName;
            Value = propertyValue;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
