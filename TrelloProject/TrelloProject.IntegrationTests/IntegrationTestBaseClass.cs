using Microsoft.AspNetCore.Http;
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
using TrelloProject.WEB.Infrastructure.ApiResponse;

namespace TrelloProject.IntegrationTests
{
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<Startup> appFactory;

       protected IntegrationTestBaseClass()
        {
            appFactory = new WebApplicationFactory<Startup>();
                
            TestClient = appFactory.CreateClient();
            
        }

       
    }

}
