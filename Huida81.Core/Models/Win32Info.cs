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

        public override string ToString()
        {
            string win32InfoString = string.Empty;
            win32InfoString += $"Name: {Name}\r\nDescription: {Description}\r\nDetails:\r\n";
            for (int i = 0; i < 113; i++)
                win32InfoString += "-";
            foreach (var detail in InfoDetails)
            {
                win32InfoString += $"\r\n|{detail.Name,35}|{detail.Value,75}|\r\n";
                for (int i = 0; i < 113; i++)
                    win32InfoString += "-";
            }
            return win32InfoString + "\r\n";
        }
    }
}
