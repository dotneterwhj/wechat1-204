using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WechatSubscription.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    PreRemindTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    NextRemindTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IntervalDays = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifyTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Creator = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifer = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reminder",
                columns: new[] { "Id", "CreateTime", "Creator", "IntervalDays", "IsDelete", "LastModifer", "LastModifyTime", "Name", "NextRemindTime", "PreRemindTime" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(7980), new TimeSpan(0, 8, 0, 0, 0)), 1, 90, false, 1, new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(8810), new TimeSpan(0, 8, 0, 0, 0)), "测试提醒事项1", new DateTimeOffset(new DateTime(2021, 9, 5, 21, 20, 7, 981, DateTimeKind.Unspecified).AddTicks(6230), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 8, 21, 20, 7, 974, DateTimeKind.Unspecified).AddTicks(4410), new TimeSpan(0, 8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Reminder",
                columns: new[] { "Id", "CreateTime", "Creator", "IntervalDays", "IsDelete", "LastModifer", "LastModifyTime", "Name", "NextRemindTime", "PreRemindTime" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 8, 0, 0, 0)), 1, 180, false, 1, new DateTimeOffset(new DateTime(2021, 6, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1720), new TimeSpan(0, 8, 0, 0, 0)), "测试提醒事项2", new DateTimeOffset(new DateTime(2021, 12, 5, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1690), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 6, 21, 20, 7, 982, DateTimeKind.Unspecified).AddTicks(1670), new TimeSpan(0, 8, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminder");
        }
    }
}
