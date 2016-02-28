using System.Collections.Generic;
using System.Threading.Tasks;
using Tookan.NET.Core;

namespace Tookan.NET.Clients
{
    public interface IAgentsClient
    {
        Task<IEnumerable<IAgent>> GetAllAsync(string lattitude, string longitude);
    }
}