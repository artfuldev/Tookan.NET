using System;
using System.Threading.Tasks;
using Tookan.NET.Core;
using Tookan.NET.Http;

namespace Tookan.NET.Clients
{
    public class TeamsClient : ApiClient, ITeamsClient
    {
        public async Task<Team> GetTeamAsync()
        {
            var uri = new Uri("/view_team", UriKind.Relative);
            var request = new {Connection.AccessToken};
            const string type = "application/json";
            var team = await Connection.Post<Team>(uri, request, type, type);
            return team.Body;
        }

        public TeamsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}