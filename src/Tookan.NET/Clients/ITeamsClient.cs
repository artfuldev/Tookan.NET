using System.Collections.Generic;
using System.Threading.Tasks;
using Tookan.NET.Core;

namespace Tookan.NET.Clients
{
    public interface ITeamsClient
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
    }
}