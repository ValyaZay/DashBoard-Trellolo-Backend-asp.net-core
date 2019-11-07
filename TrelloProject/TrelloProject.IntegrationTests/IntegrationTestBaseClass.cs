using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Extensions;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.WEB.Infrastructure.ApiResponse;

namespace TrelloProject.IntegrationTests
{
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<FakeStartup> appFactory;

       protected IntegrationTestBaseClass()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            appFactory = new TrelloFactory<FakeStartup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, conf) =>
                    {
                        conf.AddJsonFile(configPath);
                        
                    });
                    
                });
                
            TestClient = appFactory.CreateClient();
           
        }

        internal TrelloDbContext GetContext()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TrelloDbContext>();

            builder.UseSqlServer("server=DESKTOP-VU2GU8M;database=DatabaseTrelloTest100;Trusted_Connection=true")
                    .UseInternalServiceProvider(serviceProvider);

            var context = new TrelloDbContext(builder.Options);
            return context;
        }
    }

}
