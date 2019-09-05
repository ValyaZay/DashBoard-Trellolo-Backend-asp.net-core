using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class UnitTest1
    {
        private readonly HttpClient _client;

        public UnitTest1()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            var s = _client.BaseAddress.Segments;

        }

        [Fact]
        public async Task Test1()
        {
            var response = await _client.GetAsync("http://localhost:54344/api/board");

        }
    }
}
