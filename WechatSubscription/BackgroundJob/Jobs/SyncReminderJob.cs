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
            System.Console.WriteLine("��������ͬ����ʼ��" + DateTime.Now);

            using (WechatDbContext dbContext = new WechatDbContext())
            {
                List<Reminder> reminders = new List<Reminder>();

                // �����һ�����ѵ�ʱ���Ѿ��ȵ�ǰʱ��С���������´�����ʱ��
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

                    Console.WriteLine($"�ѳɹ�����idΪ{string.Join(",", reminders.Select(s => s.Id))}����������");
                }
            }

            System.Console.WriteLine("��������ͬ������ɣ�" + DateTime.Now);

            return Task.CompletedTask;
        }
    }
}