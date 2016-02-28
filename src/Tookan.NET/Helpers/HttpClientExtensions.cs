using System.Threading;
using System.Threading.Tasks;
using Tookan.NET.Http;
using Tookan.NET.Sanity;

namespace Tookan.NET.Helpers
{
    public static class HttpClientExtensions
    {
        public static Task<IResponse> Send(this IHttpClient httpClient, IRequest request)
        {
            Ensure.ArgumentIsNotNull(httpClient, "httpClient");
            Ensure.ArgumentIsNotNull(request, "request");

            return httpClient.Send(request, CancellationToken.None);
        }
    }
}