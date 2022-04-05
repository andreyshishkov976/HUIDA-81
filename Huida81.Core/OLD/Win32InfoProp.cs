namespace WMI
{
    public class Win32InfoProp
    {
        public Win32InfoProp(string propertyName, string propertyValue)
        {
            Name = propertyName;
            Value = propertyValue;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
