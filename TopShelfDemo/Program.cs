using System;
using System.Threading.Tasks;
using Topshelf;
using TopShelfDemo.Jobs;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RunTopShelfWinService();
        }

        private static async Task RunConsoleJob()
        {
            try
            {
                var service = new SpecialService();
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
                x.Service<SpecialService>(s =>
                {
                    // services want to run
                    s.ConstructUsing(name => new SpecialService());

                    // when service started
                    s.WhenStarted(async tc =>await tc.Start());

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
    }
}
