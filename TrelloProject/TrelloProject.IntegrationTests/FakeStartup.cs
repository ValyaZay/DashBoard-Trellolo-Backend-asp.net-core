
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Extensions;
using TrelloProject.DTOsAndViewModels.Exceptions;

namespace TrelloProject.IntegrationTests
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override IApplicationBuilder ConfigurateSeedDatabase(IApplicationBuilder app)
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
                    }
                    catch (Exception)
                    {
                        throw new ApiException();
                    }
                }
                
            }

            return app;
        }
    }
}
