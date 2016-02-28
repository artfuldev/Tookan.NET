using System.Collections.Generic;

namespace Tookan.NET.Core
{
    public class CustomField
    {
        public string Label { get; set; }
        public int Value { get; set; }
        public int Required { get; set; }
        public string DataType { get; set; }
        public int AppSide { get; set; }
        public string Data { get; set; }
        public string TemplateId { get; set; }
        public int ItemId { get; set; }
        public string FleetData { get; set; }
        public List<DropdownOption> Dropdown { get; set; }
    }

    public class DropdownOption
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}