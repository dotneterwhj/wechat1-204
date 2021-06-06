using System;
using System.Threading;
using System.Threading.Tasks;
using BackgroundJob.Jobs;
using Quartz;
using Quartz.Impl;

namespace BackgroundJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SyncReminderJob>()
                .WithIdentity("reminderJob", "reminderGroup")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("reminderTrigger", "reminderGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
            .Build();

            await scheduler.ScheduleJob(job, trigger);

            // You could also schedule multiple triggers for the same job with
            // await scheduler.ScheduleJob(job, new List<ITrigger>() { trigger1, trigger2 }, replace: true);
            Console.ReadKey();
        }
    }
}
