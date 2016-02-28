namespace Tookan.NET.Http
{
    public interface IApiResult<T>
    {
        string Message { get; set; }
        Status Status { get; set; }
        T Data { get; set; }
    }

    public interface IApiResult : IApiResult<object> { }
}