using System.Threading.Tasks;
using Tookan.NET.Authentication;
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly Credentials _credentials;

        public InMemoryCredentialStore(Credentials credentials)
        {
            Ensure.ArgumentIsNotNull(credentials, "credentials");

            _credentials = credentials;
        }

        public Task<Credentials> GetCredentials()
        {
            return Task.FromResult(_credentials);
        }
    }
}