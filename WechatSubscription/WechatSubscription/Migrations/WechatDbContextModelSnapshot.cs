﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WechatSubscription.DbContexts;

namespace WechatSubscription.Migrations
{
    [DbContext(typeof(WechatDbContext))]
    partial class WechatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("WechatSubscription.EFModels.Reminder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Creator")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IntervalDays")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastModifer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("NextRemindTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("PreRemindTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Reminder");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateTime = new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(7980), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 1,
                            IntervalDays = 90,
                            IsDelete = false,
                            LastModifer = 1,
                            LastModifyTime = new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(8810), new TimeSpan(0, 8, 0, 0, 0)),
                            Name = "测试提醒事项1",
                            NextRemindTime = new DateTimeOffset(new DateTime(2021, 9, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(6230), new TimeSpan(0, 8, 0, 0, 0)),
                            PreRemindTime = new DateTimeOffset(new DateTime(2021, 6, 8, 21, 20, 7, 974, DateTimeKind.Unspecified).AddTicks(4410), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CreateTime = new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 1,
                            IntervalDays = 180,
                            IsDelete = false,
                            LastModifer = 1,
                            LastModifyTime = new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1720), new TimeSpan(0, 8, 0, 0, 0)),
                            Name = "测试提醒事项2",
                            NextRemindTime = new DateTimeOffset(new DateTime(2021, 12, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1690), new TimeSpan(0, 8, 0, 0, 0)),
                            PreRemindTime = new DateTimeOffset(new DateTime(2021, 6, 6, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1670), new TimeSpan(0, 8, 0, 0, 0))
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
