using DAL.Repositories;
using DTS.Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DTS.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var tokenSettingsSection = Configuration.GetSection("TokenConfig");
            var tokenSettings = tokenSettingsSection.Get<TokenConfig>();
            services.Configure<TokenConfig>(tokenSettingsSection);
            var requestMonitorSettingsSection = Configuration.GetSection("RequestMonitorConfig");
            var requestMonitorSettings = requestMonitorSettingsSection.Get<RequestMonitorConfig>();
            services.Configure<RequestMonitorConfig>(requestMonitorSettingsSection);

            services.AddDbContext<DAL.Models.DTSLocalDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAuthServiceWrapper, AuthServiceWrapper>();
            services.AddSingleton<IConfiguration>(tokenSettingsSection);
            services.AddSingleton<RequestMonitorConfig>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "DTSAuthSpecification",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "DTSAuthAPI",
                    Version = "1"
                });
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/DTSAuthSpecification/swagger.json",
                    "Document Template System API");
                setupAction.RoutePrefix = "";
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}
