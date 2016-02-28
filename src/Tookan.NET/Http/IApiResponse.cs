namespace Tookan.NET.Http
{
    /// <summary>
    /// A response from an API call that includes the deserialized object instance
    /// and response information.
    /// </summary>
    public interface IApiResponse<out T>
    {
        /// <summary>
        /// Object deserialized from the JSON response body.
        /// </summary>
        T Body { get; }

        /// <summary>
        /// The original non-deserialized http response.
        /// </summary>
        IResponse HttpResponse { get; }

        /// <summary>
        /// Response Information like success and error messages.
        /// </summary>
        ResponseInfo ResponseInfo { get; }
    }
}