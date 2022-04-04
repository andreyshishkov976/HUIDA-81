using System;
using System.Collections.Generic;
using System.Text;

namespace Huida81.Core.Models
{
    public class Win32Info
    {
        public Win32Info(string name, string description)
        {
            Name = name;
            Description = description;
            InfoDetails = new List<Win32InfoDetail>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Win32InfoDetail> InfoDetails { get; set; }
    }
}
