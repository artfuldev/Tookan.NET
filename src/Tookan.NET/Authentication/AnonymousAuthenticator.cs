using Tookan.NET.Http;

namespace Tookan.NET.Authentication
{
    class AnonymousAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing. Retain your anonymity.
        }
    }
}
