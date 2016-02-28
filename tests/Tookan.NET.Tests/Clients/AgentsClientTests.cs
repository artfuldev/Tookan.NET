using System;
using System.Linq;
using Tookan.NET.Clients;
using Tookan.NET.Http;
using Xunit;

namespace Tookan.NET.Tests.Clients
{
    public class AgentsClientTests
    {
        private const string AccessToken = "2b997be77e2cc22becfd4c66426ef504";
        private static readonly Uri BaseUri = new Uri("https://private-anon-c562109c2-tookanapi.apiary-mock.com");
        private static readonly IConnection Connection = new Connection(BaseUri) {AccessToken = AccessToken};
        private static readonly IApiConnection ApiConnection = new ApiConnection(Connection);
        private readonly IAgentsClient _client = new AgentsClient(ApiConnection);

        [Theory]
        [InlineData("30.2221", "70.4242")]
        public void GetAllClientsWorks(string latitude, string longitude)
        {
            // Arrange
            var task = _client.GetAllAsync(latitude, longitude);

            // Act
            var agents = task.Result;

            // Assert
            Assert.NotNull(agents);
            Assert.NotEmpty(agents);
            Assert.Equal(agents.Count(), 3);
        }
    }
}