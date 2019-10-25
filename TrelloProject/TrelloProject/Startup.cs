

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.BLL.Services;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Extensions;



namespace TrelloProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddHealthChecks();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddIdentity<User, IdentityRole>(options => {
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
            //})
            
            //services.AddIdentity<User, IdentityRole>()
            //        .AddEntityFrameworkStores<TrelloDbContext>();

            
            services.AddIdentityUserAndIdentityRoleDALExtension()
                    .AddEntityFrameworkStoresDbContext();


            services.AddDbContextDALExtension(options => options.UseSqlServer(Configuration.GetConnectionString("TrelloDBConnection")));
            
            services.AddScoped<IBoardDTOService, BoardDTOService>();
            services.AddScoped<IBackgroundColorDTOService, BackgroundColorDTOService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdministrationService, AdministrationService>();

            services.AddDALDependencyInjection();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Trello API", Version = "v1" });
            });



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trello API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseMvc();
            
        }
    }
}
