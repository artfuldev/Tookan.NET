using System;
using System.Globalization;
using Tookan.NET.Http;
using Tookan.NET.Sanity;

namespace Tookan.NET.Authentication
{
    class TokenAuthenticator : IAuthenticationHandler
    {
        ///<summary>
        ///Authenticate a request using the OAuth2 Token (sent in a header) authentication scheme
        ///</summary>
        ///<param name="request">The request to authenticate</param>
        ///<param name="credentials">The credentials to attach to the request</param>
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentIsNotNull(request, "request");
            Ensure.ArgumentIsNotNull(credentials, "credentials");
            Ensure.ArgumentIsNotNull(credentials.Password, "credentials.Password");

            var token = credentials.GetToken();
            if (credentials.Login != null)
            {
                throw new InvalidOperationException("The Login is not null for a token authentication request. You " + 
                    "probably did something wrong.");
            }
            if (token != null)
            {
                request.Headers["Authorization"] = string.Format(CultureInfo.InvariantCulture, "Token {0}", token);    
            }
        }
    }
}
