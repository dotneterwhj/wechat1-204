using System;
using Microsoft.EntityFrameworkCore;
using BackgroundJob.EFModels;
using System.IO;

namespace BackgroundJob.DbContexts
{
    public class WechatDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=" + Path.Combine(AppContext.BaseDirectory, "wechat.db"));
        }

        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            return;
        }
    }

}
