using Microsoft.Extensions.DependencyInjection;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Repositories;

namespace TrelloProject.DAL.Extensions
{
    public static class DependencyInjectionDALExtension
    {
        public static IServiceCollection AddDALDependencyInjection(this IServiceCollection services)
        {
            return services.AddScoped<IBoardDTORepository, SQLBoardRepository>();
        }

    }
}
