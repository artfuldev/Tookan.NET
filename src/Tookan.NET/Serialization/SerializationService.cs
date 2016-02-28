using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tookan.NET.Serialization
{
    public class SerializationService : ISerializationService
    {
        private readonly IContractResolver _resolver = new SnakeCaseJsonContractResolver();
        private JsonSerializerSettings Settings => new JsonSerializerSettings {ContractResolver = _resolver};
        public string Serialize(object value) => JsonConvert.SerializeObject(value, Settings);
        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value, Settings);
        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value, Settings);
        public object Deserialize(string value) => JsonConvert.DeserializeObject<object>(value, Settings);
    }
}