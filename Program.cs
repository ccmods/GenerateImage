using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Entity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GenerateScreen
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            CancellationToken ct = CancellationToken.None;
            // Run app
            await serviceProvider.GetService<GenerateImageService>().StartAsync(ct);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            // Add logging
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddSerilog();
                loggingBuilder.AddDebug();
            });

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(Path.Combine(configuration["logOptions:basePath"],"genImg-.txt"), rollingInterval: RollingInterval.Day,
                    outputTemplate: configuration["logOptions:format"])
                .CreateLogger();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton(configuration);
            serviceCollection.Configure<AppConfig>(configuration.GetSection("appSettings"));
            // Add app
            serviceCollection.AddTransient<GenerateImageService>();
        }
    }            
}
