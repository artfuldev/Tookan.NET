namespace Tookan.NET.Serialization
{
    public interface ISerializationService
    {
        string Serialize(object value);
        string Serialize<T>(T value);
        T Deserialize<T>(string value);
        object Deserialize(string value);
    }
}