using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopShelfDemo.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace TopShelfDemo
{
    public class SelfHostBuilder
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
            {

                //services.AddHostedService<DoWorkHostedServices>();
                services.AddHostedService<MyBackgroundServices>();
            });
        }
    }
}
