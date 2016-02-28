#if DNXCORE50 || DOTNET5_4

// ReSharper disable once CheckNamespace
// This is a polyfill
namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute:Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}

#endif