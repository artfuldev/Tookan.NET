using System;
using System.Collections.Generic;
using System.Linq;
using Tookan.NET.Helpers;
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    internal static class ApiInfoParser
    {
        public static ApiInfo ParseResponseHeaders(IDictionary<string, string> responseHeaders)
        {
            Ensure.ArgumentIsNotNull(responseHeaders, "responseHeaders");
            var oauthScopes = new List<string>();
            var acceptedOauthScopes = new List<string>();
            string etag = null;

            if (responseHeaders.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                acceptedOauthScopes.AddRange(responseHeaders["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (responseHeaders.ContainsKey("X-OAuth-Scopes"))
            {
                oauthScopes.AddRange(responseHeaders["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (responseHeaders.ContainsKey("ETag"))
            {
                etag = responseHeaders["ETag"];
            }

            return new ApiInfo(oauthScopes, acceptedOauthScopes, etag, new RateLimit(responseHeaders));
        }
    }
}
