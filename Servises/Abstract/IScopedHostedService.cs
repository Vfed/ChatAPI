using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Servises.Specific
{
    public interface IScopedHostedService
    {
        public Task GetDataTimeAsync(CancellationToken stoppingToken);
    }
}