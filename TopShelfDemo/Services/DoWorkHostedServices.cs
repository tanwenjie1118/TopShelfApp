using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TopShelfDemo.Services
{
    public class DoWorkHostedServices : IHostedService, IDisposable
    {
        private Timer timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting work");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(0.5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Console.WriteLine("Doing work");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine("Stopping work");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
