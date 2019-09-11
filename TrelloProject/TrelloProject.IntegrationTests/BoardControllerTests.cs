using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class BoardControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BoardControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(ApiRoutes.Board.GetAll);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetById_EndpointsReturnSuccessAndCorrectContentType()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(ApiRoutes.Board.GetById);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact(Skip ="An error is inside")]
        public async Task Post_EndpointsReturnSuccessAndCorrectContentTypeAndLocation()
        {
            //Arrange
            var client = _factory.CreateClient();
             
            var content = new BoardCreateViewModel() { Title = "Test222Post", CurrentBackgroundColorId = BgColorEnum.Blue };

            var stringContent = new StringContent(content.ToString());


            //Act
            var response = await client.PostAsync("http://localhost:54344/api/board", stringContent);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("http://localhost:54344/api/board", response.Content.Headers.ContentLocation.ToString());
        }
    }
}
