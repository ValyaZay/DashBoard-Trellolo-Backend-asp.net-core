using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;
using TrelloProject.WEB.Contracts.V1;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using TrelloProject.DTOsAndViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using TrelloProject.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace TrelloProject.IntegrationTests
{
    public class BgColorControllerTests : IntegrationTestBaseClass
    {
        [Fact]
        public async Task Get_IfTheDBIsNotEmpty_ReturnsOKResponseAndCorrectCount()
        {
            //Arrange

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.BackgroundColor.GetAll);
            
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedCollection = await response.Content.ReadAsAsync<List<BgColorViewModel>>();
            returnedCollection.Count.Should().BeGreaterThan(0);
            returnedCollection.Should().NotBeNullOrEmpty();
            returnedCollection.Should().BeOfType<List<BgColorViewModel>>();
            returnedCollection.Should().NotContainNulls();

        }

        private HttpClient TestClientInMemory;
        [Fact]
        public async Task Get_IfTheDBIsEmpty_ReturnsOKResponseWithMessage()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(TrelloDbContext));
                        services.AddDbContext<TrelloDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("Get_IfTheDBIsEmpty_ReturnsOKResponseWithMessage");
                        });
                    });
                });
            TestClientInMemory = appFactory.CreateClient();

            // Act
            var response = await TestClientInMemory.GetAsync(ApiRoutes.BackgroundColor.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("There are no BgColor created.");
        }
    }
}
