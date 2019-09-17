using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DAL.Extensions;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;

namespace TrelloProject.IntegrationTests
{
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<Startup> appFactory;

       protected IntegrationTestBaseClass()
        {
            appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(buidler =>
                {
                    buidler.ConfigureServices(services =>
                    {
                        services.RemoveDbContextDALExtension();
                        services.AddDbContextDALExtension(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
            
        }

        protected void DisposeAppFactory()
        {
            appFactory.Dispose(); 
        }

        protected async Task<BoardDTO> CreateBoardAsync(BoardCreateViewModel boardCreateViewModel)
        {
            var response =  await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);
            return await response.Content.ReadAsAsync<BoardDTO>();
        }

        protected async Task SeedContext(BoardCreateViewModel boardCreateViewModel)
        {
            await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);
            
        }

    }

}
