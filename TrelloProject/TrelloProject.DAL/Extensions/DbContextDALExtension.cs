using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;

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

        public static IdentityBuilder AddEntityFrameworkStoresDbContext(this IdentityBuilder identityBuilder)
        {
            return identityBuilder.AddEntityFrameworkStores<TrelloDbContext>();
             
        }

        public static IdentityBuilder AddIdentityUserAndIdentityRoleDALExtension(this IServiceCollection services)
        {
            return services.AddIdentity<User,IdentityRole>();
        }
    }
}
