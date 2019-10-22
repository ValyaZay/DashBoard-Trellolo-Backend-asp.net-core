using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Repositories;

namespace TrelloProject.DAL.Extensions
{
    public static class DependencyInjectionDALExtension
    {
        public static IServiceCollection AddDALDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBoardDTORepository, SQLBoardRepository>();
            services.AddScoped<IBackgroundColorDTORepository, SQLBackgroundColorRepository>();
            services.AddScoped<IAccountRepository, SQLAccountRepository>();
            services.AddScoped<IAdministrationRepository, SQLAdministrationRepository>();
            
            return services;
        }

    }
}
