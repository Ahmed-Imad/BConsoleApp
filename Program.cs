using Microsoft.Extensions.Configuration;
using System;
using System.IO;
// Dependency Injection, Serilog (logging), Settings Tamplete for Console App projects.
namespace BConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Serilog
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application is running now!");

            //Dependency Injection
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IRunningService, RunningService>();
                })
                .UseSerilog()
                .Build();

            // Normaly when you ask for dependency injection to give you an instance you
            // want to use the Interface but for starting the app you need class (concrete type).
            var svc = ActivatorUtilities.CreateInstance<RunningService>(host.Services);
            svc.Run();
        }

        //Serilog
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
                
        }
    }
}
