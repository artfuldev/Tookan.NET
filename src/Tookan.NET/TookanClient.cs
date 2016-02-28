using System;
using Tookan.NET.Clients;
using Tookan.NET.Http;

namespace Tookan.NET
{
    public class TookanClient : ITookanClient
    {
        internal static readonly Uri BaseUri = new Uri("https://api.tookanapp.com:8888");

        public TookanClient(string accessToken) : this(new Connection(BaseUri) {AccessToken = accessToken})
        {
        }

        internal TookanClient(IConnection connection)
        {
            var apiConnection = new ApiConnection(connection);
            Agents = new AgentsClient(apiConnection);
            Teams = new TeamsClient(apiConnection);
        }

        public IAgentsClient Agents { get; }
        public ITeamsClient Teams { get; }
    }
}