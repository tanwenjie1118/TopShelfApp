using Autofac;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TopShelfDemo.Services
{
    public class MyBackgroundServices : BackgroundService
    {
        private string words { set; get; } = "My background services running";
        public void PrintWords(string words)
        {
            Console.WriteLine(words);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Console.WriteLine(words);

            return Task.CompletedTask;
        }
    }
}
