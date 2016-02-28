namespace Tookan.NET.Http
{
    public class ApiResult<T> : IApiResult<T>
    {
        public string Message { get; set; }
        public Status Status { get; set; }
        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object>, IApiResult { }
}