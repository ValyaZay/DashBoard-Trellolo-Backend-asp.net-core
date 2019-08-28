using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.Interfaces;
using TrelloProject.DAL.Repositories;

namespace TrelloProject.BLL.Extensions
{
    public static class DependencyInjectionBllExtension
    {
        public static IServiceCollection AddBLLDependencyInjection(this IServiceCollection services)
        {
            return services.AddScoped<IBoardRepository, SQLBoardRepository>();
        }
    }
}
