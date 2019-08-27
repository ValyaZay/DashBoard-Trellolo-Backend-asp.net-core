using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;

namespace TrelloProject.BLL.Extensions
{
     public static class DbContextBllExtension
     {
        public static IServiceCollection AddDbContextBllExtension(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddDbContext<TrelloDbContext>(options);
        }
     }
}
