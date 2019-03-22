using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DAL.Data;
using DAL.Models;
using NLog.Web;

namespace DTS
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            var host = CreateWebHostBuilder(args).UseUrls("https://localhost:44346").Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DTSLocalDBContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                }
            }
    
            host.Run();
        
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
