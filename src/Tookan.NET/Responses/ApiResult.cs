namespace Tookan.NET.Responses
{
    public class ApiResult<T> : IApiResult<T>
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object>, IApiResult { }
}