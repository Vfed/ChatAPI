using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Servises.Specific
{
    //public class TimerBackground
    //{
    //}

    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger<TimedHostedService> _logger;
        public IServiceProvider _services { get; }

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
        {
            _services = services;
            _logger = logger;
        }
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {

            using (var scope = _services.CreateScope())
            {
                var scopedHostedService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedHostedService>();

                await scopedHostedService.GetDataTimeAsync(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }

        
    }
}