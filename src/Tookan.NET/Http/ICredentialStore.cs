using System.Threading.Tasks;
using Tookan.NET.Authentication;

namespace Tookan.NET.Http
{
    public interface ICredentialStore
    {
        Task<Credentials> GetCredentials();
    }
}
