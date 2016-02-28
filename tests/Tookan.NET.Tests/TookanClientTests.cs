using System;
using Tookan.NET.Http;

namespace Tookan.NET.Tests
{
    public class TookanClientTests
    {
        private const string AccessToken = "2b997be77e2cc22becfd4c66426ef504";
        private static readonly Uri BaseUri = new Uri("https://private-anon-c562109c2-tookanapi.apiary-mock.com");
        private static readonly IConnection Connection = new Connection(BaseUri) { AccessToken = AccessToken };
        private readonly ITookanClient _client = new TookanClient(Connection);


    }
}