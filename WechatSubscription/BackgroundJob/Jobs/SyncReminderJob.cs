using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using BackgroundJob.DbContexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BackgroundJob.EFModels;

namespace BackgroundJob.Jobs
{
    public class SyncReminderJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine("提醒事项同步开始：" + DateTime.Now);

            using (WechatDbContext dbContext = new WechatDbContext())
            {
                List<Reminder> reminders = new List<Reminder>();

                // 如果下一次提醒的时间已经比当前时间小，则重置下次提醒时间
                foreach (var reminder in dbContext.Reminders.AsNoTracking().Where(r => !r.IsDelete).ToList().Where(r => r.NextRemindTime < DateTimeOffset.Now))
                {
                    reminder.PreRemindTime = reminder.NextRemindTime;
                    reminder.NextRemindTime = reminder.NextRemindTime.AddDays(reminder.IntervalDays);
                    reminder.LastModifyTime = DateTimeOffset.Now;

                    reminders.Add(reminder);
                }

                if (reminders.Count > 0)
                {
                    dbContext.UpdateRange(reminders.ToArray());

                    dbContext.SaveChanges();

                    Console.WriteLine($"已成功重置id为{string.Join(",", reminders.Select(s => s.Id))}的提醒事项");
                }
            }

            System.Console.WriteLine("提醒事项同步已完成：" + DateTime.Now);

            return Task.CompletedTask;
        }
    }
}