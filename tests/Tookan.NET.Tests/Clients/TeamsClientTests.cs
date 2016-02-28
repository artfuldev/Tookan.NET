using System;
using System.Linq;
using Tookan.NET.Clients;
using Tookan.NET.Http;
using Xunit;

namespace Tookan.NET.Tests.Clients
{
    public class TeamsClientTests
    {
        private const string AccessToken = "7eeca84b5e1df27d15f8422949314db6";
        private static readonly Uri BaseUri = new Uri("https://private-anon-c562109c2-tookanapi.apiary-mock.com");
        private static readonly IConnection Connection = new Connection(BaseUri) {AccessToken = AccessToken};
        private static readonly IApiConnection ApiConnection = new ApiConnection(Connection);
        private readonly ITeamsClient _client = new TeamsClient(ApiConnection);

        [Fact]
        public void GetTeamsWorks()
        {
            // Arrange
            var task = _client.GetTeamsAsync();

            // Act
            var teams = task.Result;

            // Assert
            Assert.NotNull(teams);
            Assert.NotEmpty(teams);
            Assert.Equal(teams.Count(), 3);
        }
    }
}