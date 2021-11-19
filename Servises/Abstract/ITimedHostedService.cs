using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Servises.Specific
{
    public interface ITimedHostedService
    {
        void Dispose();
        Task StartAsync(CancellationToken stoppingToken);
        Task StopAsync(CancellationToken stoppingToken);
        public int GetCounter();
    }
}