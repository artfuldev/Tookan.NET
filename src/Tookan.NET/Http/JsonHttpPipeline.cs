using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using Tookan.NET.Helpers;
using Tookan.NET.Sanity;
using Tookan.NET.Serialization;

namespace Tookan.NET.Http
{
    /// <summary>
    ///     Responsible for serializing the request and response as JSON and
    ///     adding the proper JSON response header.
    /// </summary>
    public class JsonHttpPipeline : IJsonHttpPipeline
    {
        readonly ISerializationService _serializer;

        public JsonHttpPipeline() : this(new SerializationService())
        {
        }

        public JsonHttpPipeline(ISerializationService serializer)
        {
            Ensure.ArgumentIsNotNull(serializer, "serializer");

            _serializer = serializer;
        }

        public void SerializeRequest(IRequest request)
        {
            Ensure.ArgumentIsNotNull(request, "request");

            if (!request.Headers.ContainsKey("Accept"))
            {

                request.Headers["Accept"] = "application/json";
            }
            
            if (request.Method == HttpMethod.Get || request.Body == null) return;
            if (request.Body is string || request.Body is Stream || request.Body is HttpContent) return;

            request.Body = _serializer.Serialize((dynamic) request.Body);
        }

        public IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            Ensure.ArgumentIsNotNull(response, "response");

            if (response.ContentType == null ||
                !response.ContentType.Equals("application/json", StringComparison.Ordinal))
                return new ApiResponse<T>(response);

            var body = response.Body as string;
            var result = _serializer.Deserialize<ApiResult<T>>(body);
            return new ApiResponse<T>(response,
                result.Data,
                new ResponseInfo() {Message = result.Message, Status = result.Status});
        }
    }
}
