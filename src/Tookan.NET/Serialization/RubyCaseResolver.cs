using Newtonsoft.Json.Serialization;
using Tookan.NET.Helpers;

namespace Tookan.NET.Serialization
{
    internal class RubyCaseResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToRubyCase();
        }
    }
}