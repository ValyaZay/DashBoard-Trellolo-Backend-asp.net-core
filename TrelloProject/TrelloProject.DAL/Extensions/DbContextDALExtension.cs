using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;

namespace TrelloProject.DAL.Extensions
{
    public static class DbContextDALExtension
    {
        public static IServiceCollection AddDbContextDALExtension(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddDbContext<TrelloDbContext>(options);
        }

        public static IServiceCollection RemoveDbContextDALExtension(this IServiceCollection services)
        {
            return services.RemoveAll(typeof(TrelloDbContext));
        }
    }
}
