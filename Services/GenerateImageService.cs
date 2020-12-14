using System;
using System.Threading;
using System.Threading.Tasks;
using Entity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Services
{
    internal class GenerateImageService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<AppConfig> _appConfig;

        public GenerateImageService(ILogger<GenerateImageService> logger, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _appConfig = appConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting");
            DoWork(cancellationToken);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            string msImgUrl = BC.WebBC.getBackgrndImgUrl(_appConfig.Value.msImageUrl);
              /*root.name - "Troy"
                    Weather[0].main - "Clouds".
                    Weather[0].description - scattered clouds
                    root.main.feels_like
                    "" "" temp
                    "" "" temp_max
                    "" "" temp_min*/                
            WeatherRoot wr = BC.WebBC.getWeatherData(_appConfig.Value.openWeatherUrl,_appConfig.Value.openWeatherKey);
            string r = "";
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {        
        }
    }   
}
