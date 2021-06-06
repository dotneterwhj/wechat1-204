using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
namespace BackgroundJob.Jobs
{
    public class SyncReminderJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine("hello world");
            return Task.CompletedTask;
        }
    }
}