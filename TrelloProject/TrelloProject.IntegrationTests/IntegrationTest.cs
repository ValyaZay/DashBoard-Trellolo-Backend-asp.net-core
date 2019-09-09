using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class IntegrationTest
    {
        private readonly HttpClient _client;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            var s = _client.BaseAddress.Segments;

        }

        [Fact]
        public async Task Get()
        {
            var response = await _client.GetAsync("http://localhost:54344/api/board");

        }


    }
}
