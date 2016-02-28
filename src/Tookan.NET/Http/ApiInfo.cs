using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo(IList<string> oauthScopes,
            IList<string> acceptedOauthScopes,
            string etag,
            RateLimit rateLimit)
        {
            Ensure.ArgumentIsNotNull(oauthScopes, "oauthScopes");

            OauthScopes = new ReadOnlyCollection<string>(oauthScopes);
            AcceptedOauthScopes = new ReadOnlyCollection<string>(acceptedOauthScopes);
            Etag = etag;
            RateLimit = rateLimit;
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public IReadOnlyList<string> OauthScopes { get; private set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public IReadOnlyList<string> AcceptedOauthScopes { get; private set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; private set; }

        /// <summary>
        /// Information about the API rate limit
        /// </summary>
        public RateLimit RateLimit { get; private set; }
    }
}
