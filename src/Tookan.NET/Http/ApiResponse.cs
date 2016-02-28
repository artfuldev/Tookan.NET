
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse(IResponse response) : this(response, GetBodyAsObject(response))
        {
        }

        public ApiResponse(IResponse response, T bodyAsObject, ResponseInfo responseInfo = null)
        {
            Ensure.ArgumentIsNotNull(response, "response");

            HttpResponse = response;
            Body = bodyAsObject;
            ResponseInfo = responseInfo;
        }

        public T Body { get; private set; }

        public IResponse HttpResponse { get; private set; }

        private static T GetBodyAsObject(IResponse response)
        {
            var body = response.Body;
            if (body is T) return (T)body;
            return default(T);
        }

        public ResponseInfo ResponseInfo { get; private set; }
    }
}
