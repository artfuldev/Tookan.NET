namespace Tookan.NET.Responses
{
    public interface IApiResult<T>
    {
        string Message { get; set; }
        int Status { get; set; }
        T Data { get; set; }
    }

    public interface IApiResult : IApiResult<object> { }
}