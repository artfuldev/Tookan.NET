using Newtonsoft.Json.Serialization;

namespace Tookan.NET.Serialization
{
    internal class SnakeCaseJsonContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToSnakeCase();
        }
    }
}