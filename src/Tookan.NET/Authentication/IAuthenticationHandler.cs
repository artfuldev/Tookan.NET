using Tookan.NET.Http;

namespace Tookan.NET.Authentication
{
    interface IAuthenticationHandler
    {
        void Authenticate(IRequest request, Credentials credentials);
    }
}