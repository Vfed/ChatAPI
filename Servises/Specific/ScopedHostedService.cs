using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Servises.Specific
{
    public class ScopedHostedService : IScopedHostedService
    {
        private readonly IServiceProvider _services;
        private readonly DbService _dbService;
        public ScopedHostedService(IServiceProvider service, DbService dbService)
        {
            _services = service;
            _dbService = dbService;
        }

        public async Task GetDataTimeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var datatime = _dbService.ChatUsers.Select(x => x.LastActionTime).ToList();
                    var localDataService =
                        _services.GetRequiredService<ILocaLDataService>();
                int count = 0;

                DateTime centuryBegin = new DateTime(2001, 1, 1);
                DateTime currentDate = DateTime.Now;

                long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                long ticks = (long)elapsedSpan.TotalMinutes;

                foreach (var item in datatime)
                {
                    long userTicks = centuryBegin.Ticks > item.Ticks ? 0 : item.Ticks - centuryBegin.Ticks;
                    TimeSpan userSpan = new TimeSpan(userTicks);
                    long userMinutes = (long)userSpan.TotalMinutes;
                    if (ticks - 10 < userMinutes)
                    {
                        count++;
                    }
                }

                localDataService.SetUsersOnline(count);
                var i = localDataService.GetUsersOnline();
            await Task.Delay(30000, stoppingToken);
            }
            
        }

    }
}
