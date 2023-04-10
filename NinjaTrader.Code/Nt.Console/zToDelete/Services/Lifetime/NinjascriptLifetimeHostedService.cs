using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class NinjascriptLifetimeHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IHostEnvironment _environment;
        private readonly ILogger _logger;

        public NinjascriptLifetimeHostedService(IHostApplicationLifetime appLifetime, ILogger<NinjascriptLifetimeHostedService> logger, IHostEnvironment environment)
        {
            _applicationLifetime = appLifetime;
            _environment = environment;
            _logger = logger;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(DataLoad);
            _applicationLifetime.ApplicationStopping.Register(OnStopping);
            _applicationLifetime.ApplicationStopped.Register(Terminated);
            
            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) 
            => Task.CompletedTask;

        private void DataLoad()
        {
            _logger.LogInformation("The ninjascript started.");
        }

        private void OnStopping()
        {
            _logger.LogInformation("The ninjascript is stopping");
        }

        private void Terminated()
        {
            _logger.LogInformation("The ninjascript terminated.");
        }
    }
}
