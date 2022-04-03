using System.Management;
using System.Reflection;

namespace Huida81_WPF.Core.Models
{
    public class Win32InfoBase : IWin32Info
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public Win32InfoBase(ManagementObject managementObject)
        {
            

            foreach (var prop in managementObject.Properties)
            {
                var match = GetType().GetProperty(prop.Name);
                if (match != null)
                    match.SetValue(this, prop.Value);
            }
        }
    }
}