using System.Net.Http;

namespace Tookan.NET
{
    public static class HttpVerb
    {
        public static HttpMethod Patch { get; } = new HttpMethod("PATCH");
    }
}
