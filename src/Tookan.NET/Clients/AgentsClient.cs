using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tookan.NET.Core;
using Tookan.NET.Http;

namespace Tookan.NET.Clients
{
    internal class AgentsClient : ApiClient, IAgentsClient
    {
        public AgentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public async Task<IEnumerable<IAgent>> GetAllAsync(string lattitude = null, string longitude = null)
        {
            var uri = new Uri("/view_all_fleets_location", UriKind.Relative);
            var request = new
            {
                Latitude = lattitude,
                Longitude = longitude,
                Connection.AccessToken
            };
            const string type = "application/json";
            var agents = await Connection.Post<List<Agent>>(uri, request, type, type);
            return agents.Body;
        }
    }
}
