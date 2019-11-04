using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DTOsAndViewModels.Exceptions;

namespace TrelloProject.DAL.Extensions
{
    public static class DbContextDALExtension
    {

        // Configuration

        public static IServiceCollection AddDbContextDALExtension(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddDbContext<TrelloDbContext>(options);
        }

        public static IServiceCollection RemoveDbContextDALExtension(this IServiceCollection services)
        {
            return services.RemoveAll(typeof(TrelloDbContext));
        }


        public static IdentityBuilder AddIdentityUserAndIdentityRoleDALExtension(this IServiceCollection services)
        {
            return services.AddIdentity<User,IdentityRole>();
        }

        // Using

        public static IdentityBuilder AddEntityFrameworkStoresDbContext(this IdentityBuilder identityBuilder)
        {
            return identityBuilder.AddEntityFrameworkStores<TrelloDbContext>();

        }

        public static IApplicationBuilder MigrateAndSeedDatabase(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TrelloDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                        int countBgColors = context.BackgroundColors.Count();
                        if (countBgColors == 0)
                        {
                            context.BackgroundColors.Add(new BackgroundColor() { ColorHex = "#C0C0C0", ColorName = "Grey" });
                            context.BackgroundColors.Add(new BackgroundColor() { ColorHex = "#ffff00", ColorName = "Yellow" });
                            context.BackgroundColors.Add(new BackgroundColor() { ColorHex = "#FFA500", ColorName = "Orange" });
                            context.BackgroundColors.Add(new BackgroundColor() { ColorHex = "#0000FF", ColorName = "Blue" });
                            context.BackgroundColors.Add(new BackgroundColor() { ColorHex = "#008000", ColorName = "Green" });

                            context.SaveChanges();
                        }

                        int countBoards = context.Boards.Count();
                        if (countBoards == 0)
                        {
                            
                            context.Boards.Add(new Board { Title = "Test", CurrentBackgroundColorId = 2 });
                            context.SaveChanges();
                        }

                        int countCardLists = context.CardLists.Count();
                        if (countCardLists == 0)
                        {
                            context.CardLists.Add(new CardList() { Title = "ToDo", Order = 0, BoardId = 1 });
                            context.CardLists.Add(new CardList() { Title = "Done", Order = 1, BoardId = 1 });
                            context.CardLists.Add(new CardList() { Title = "DoNOTDo", Order = 2, BoardId = 1 });
                            context.SaveChanges();
                        }

                        int countCards = context.Cards.Count();
                        if (countCards == 0)
                        {
                            context.Cards.Add(new Card() { Title = "Create a task", Description = "Just create a new task", CreatedDate = DateTime.Parse("2019-08-23"), Hidden = false, CardListId = 1 });
                            context.Cards.Add(new Card() { Title = "Implement an Interface", Description = "Implement INewInterface now", CreatedDate = DateTime.Parse("2019-08-15"), Hidden = false, CardListId = 1 });

                            context.SaveChanges();
                        }

                        int countUsers = context.Users.Count();
                        if (countUsers == 0)
                        {
                            context.Users.Add(new User() { FirstName = "Valya", LastName = "Zay", Email = "valya@valya.net" });
                            context.Users.Add(new User() { FirstName = "Vova", LastName = "Petrov", Email = "vova@vova.com" });
                            context.Users.Add(new User() { FirstName = "Gora", LastName = "Sidorov", Email = "gora@gora.net" });

                            context.SaveChanges();
                        }

                        int countCardComments = context.CardComments.Count();
                        if (countCardComments == 0)
                        {
                            context.CardComments.Add(new CardComment() { Text = "Good comment", CreatedDate = DateTime.Parse("2019-02-15"), CardId = 2 });
                            context.CardComments.Add(new CardComment() { Text = "Bad comment", CreatedDate = DateTime.Parse("2019-04-18"), CardId = 3 });
                            
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
              
            }
            
            return app;
        }
    }
}
