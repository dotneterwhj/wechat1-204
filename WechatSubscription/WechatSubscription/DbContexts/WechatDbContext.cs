using System;
using Microsoft.EntityFrameworkCore;
using WechatSubscription.EFModels;

namespace WechatSubscription.DbContexts
{
    public class WechatDbContext : DbContext
    {
        public WechatDbContext(DbContextOptions<WechatDbContext> options) : base(options)
        {

        }

        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            return;
            modelBuilder.Entity<Reminder>().HasData(
                new Reminder
                {
                    Id = 1,
                    Name = "测试提醒事项1",
                    PreRemindTime = new System.DateTimeOffset(DateTime.Now.AddDays(3)),
                    NextRemindTime = new DateTimeOffset(DateTime.Now.AddMonths(3)),
                    IntervalDays = 90,
                    CreateTime = DateTime.Now,
                    LastModifyTime = DateTime.Now,
                    Creator = "1",
                    LastModifer = "1",
                    IsDelete = false
                },
                new Reminder
                {
                    Id = 2,
                    Name = "测试提醒事项2",
                    PreRemindTime = new System.DateTimeOffset(DateTime.Now.AddDays(1)),
                    NextRemindTime = new DateTimeOffset(DateTime.Now.AddMonths(6)),
                    IntervalDays = 180,
                    CreateTime = DateTime.Now,
                    LastModifyTime = DateTime.Now,
                    Creator = "1",
                    LastModifer = "1",
                    IsDelete = false
                }
            );
        }
    }

}
