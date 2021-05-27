using Autofac;
using Quartz;
using Quartz.Impl;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TopShelfDemo.Services
{
    public class QuartzService
    {
        private IScheduler scheduler;
        public QuartzService() {
            if (scheduler == null)
            {
                // Grab the Scheduler instance from the Factory
                StdSchedulerFactory factory = new StdSchedulerFactory();
                scheduler = factory.GetScheduler().Result;
            }
        }

        public async Task Start()
        {
            await RunJobs();
        }

        public async Task Stop()
        {
            //To do something
            await scheduler.Shutdown();
        }

        private async Task RunJobs()
        {
            var builder = JobBuilder.Create<JobConsole>();
            var job = builder.Build();

            var triggerBuilder = TriggerBuilder.Create();
            triggerBuilder.WithCronSchedule("0/10 * * * * ?");
            var trigger = triggerBuilder.Build();

            await scheduler.ScheduleJob(job, trigger);

            // and start it off
            await scheduler.Start();

            // some sleep to show what's happening
            //await Task.Delay(TimeSpan.FromSeconds(10));

            // and last shut down the scheduler when you are ready to close your program
            //await scheduler.Shutdown();
        }

        private void CallApi(string url)
        {
            IRestClient client = new RestClient
            {
                BaseUrl = new Uri(url)
            };
            IRestRequest request = new RestRequest();

            client.Post(request);
        }

        private class JobConsole : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                // Console.WriteLine("Now is time :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ok.txt");
                File.AppendAllLines(path, new List<string> { "This is time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }); ;
            }
        }
    }
}
