using Autofac;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using TopShelfDemo;
using TopShelfDemo.Services;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static readonly CancellationToken canceltoken = cancellationTokenSource.Token;
        static async Task Main(string[] args)
        {
            //RunTopShelfWinService();

            var taskcancel = StopConsole();
            var taskrunning = RunSelfHostServices(args);

            Task.WaitAll(taskrunning, taskcancel);

        }

        private static async Task RunConsoleJob()
        {
            try
            {
                var service = new QuartzService();
                await service.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.Read();
        }

        private static void RunTopShelfWinService()
        {
            // configurating service factory
            HostFactory.Run(x =>
            {
                x.Service<QuartzService>(s =>
                {
                    // services want to run
                    s.ConstructUsing(name => new QuartzService());

                    // when service started
                    s.WhenStarted(async tc => await tc.Start());

                    // when service stopped
                    s.WhenStopped(async tc => await tc.Stop());
                });

                // How to run
                x.RunAsLocalSystem();

                // Description
                x.SetDescription("A demo of TopShelf application");

                // Display name
                x.SetDisplayName("TopShelfJobService");

                // Service name
                x.SetServiceName("TopShelfJobService");
            });
        }


        private static Task RunSelfHostServices(string[] args)
        {
            using var host = SelfHostBuilder.CreateHostBuilder(args).Build();
            return host.RunAsync(canceltoken);
        }

        private static Task StopConsole()
        {
            //return Task.CompletedTask;
            return new TaskFactory().StartNew(() =>
             {
                 Thread.Sleep(15000);
                 cancellationTokenSource.Cancel();
             });

        }
    }
}
