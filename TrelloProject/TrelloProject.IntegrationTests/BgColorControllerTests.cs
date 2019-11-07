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
using TrelloProject.WEB.Infrastructure.ApiResponse;
using Newtonsoft.Json;

namespace TrelloProject.IntegrationTests
{
    public class BgColorControllerTests : IntegrationTestBaseClass
    {
        [Fact]
        public async Task Get_IfTheDBIsNotEmpty_ReturnsOKResponseAndCorrectCount()
        {
            //Arrange
            
            // Act
            var responseMessageGet = await TestClient.GetAsync(ApiRoutes.BackgroundColor.GetAll);
            var jsonStringGet = await responseMessageGet.Content.ReadAsStringAsync();
            var apiResponseGet = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGet.Result.ToString();
            var apiResult = JsonConvert.DeserializeObject<List<BgColorViewModel>>(jsonStringResult);
            int apiResultCount = apiResult.Count;

            // Assert
            apiResult.Should().BeOfType<List<BgColorViewModel>>();
            apiResult.Should().HaveCount(apiResultCount);
            apiResponseGet.StatusCode.Should().Be(200);
            apiResponseGet.CustomCode.Should().Be(0);
            apiResponseGet.Should().NotBeNull();
        }

        private HttpClient TestClientInMemory;
        [Fact]
        public async Task Get_IfTheDBIsEmpty_ReturnsOKResponseAndZeroCount()
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
            var responseMessageGet = await TestClientInMemory.GetAsync(ApiRoutes.BackgroundColor.GetAll);
            var jsonStringGet = await responseMessageGet.Content.ReadAsStringAsync();
            var apiResponseGet = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGet.Result.ToString();
            var apiResult = JsonConvert.DeserializeObject<List<BgColorViewModel>>(jsonStringResult);
            int apiResultCount = apiResult.Count;

            // Assert
            apiResult.Should().BeOfType<List<BgColorViewModel>>();
            apiResult.Should().HaveCount(apiResultCount);
            apiResponseGet.StatusCode.Should().Be(200);
            apiResponseGet.CustomCode.Should().Be(0);
            apiResponseGet.Should().NotBeNull();
        }
    }
}
