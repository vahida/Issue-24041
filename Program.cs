using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BareBoneMembershipApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(Configuration)
            //    .Destructure.UsingAttributes()
            //    .CreateLogger();

            try
            {
                //Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                //Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //Host.CreateDefaultBuilder(args)
            //    .UseSerilog()
            //    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>()
            //            .UseHealthChecks("/health");
            //    });
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
