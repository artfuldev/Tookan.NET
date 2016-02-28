using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using Tookan.NET.Http;
using Tookan.NET.Sanity;

namespace Tookan.NET.Authentication
{
    class BasicAuthenticator : IAuthenticationHandler
    {
        ///<summary>
        ///Authenticate a request using the basic access authentication scheme
        ///</summary>
        ///<param name="request">The request to authenticate</param>
        ///<param name="credentials">The credentials to attach to the request</param>
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentIsNotNull(request, "request");
            Ensure.ArgumentIsNotNull(credentials, "credentials");
            Ensure.ArgumentIsNotNull(credentials.Login, "credentials.Login");
            Debug.Assert(credentials.Password != null, "It should be impossible for the password to be null");

            var encodedLogin = WebUtility.UrlEncode(credentials.Login);
            var encodedPassword = WebUtility.UrlEncode(credentials.Password);
            var formattedString = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", encodedLogin, encodedPassword);
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(formattedString));
            var header = string.Format(CultureInfo.InvariantCulture, "Basic {0}", base64String);

            request.Headers["Authorization"] = header;
        }
    }
}
