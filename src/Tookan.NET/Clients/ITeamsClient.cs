using System.Threading.Tasks;
using Tookan.NET.Core;

namespace Tookan.NET.Clients
{
    public interface ITeamsClient
    {
        Task<Team> GetTeamAsync();
    }
}