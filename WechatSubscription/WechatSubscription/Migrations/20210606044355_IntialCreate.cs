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
                    Creator = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    LastModifer = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminder");
        }
    }
}
