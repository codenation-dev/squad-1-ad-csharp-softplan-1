using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErros.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "error_log",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Archieved = table.Column<bool>(nullable: false),
                    Environment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Active", "CreatedAt", "Email", "Login", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("f84b0b04-7843-46a7-99bb-40733f588de6"), true, new DateTime(2019, 12, 15, 20, 31, 8, 246, DateTimeKind.Utc).AddTicks(4098), "admin@centraldeerros.com", "Admin", "Administrator", "db7f0145c4665ea116fdf931bc69aa0f", "Admin", new DateTime(2019, 12, 15, 20, 31, 8, 250, DateTimeKind.Utc).AddTicks(8987) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "error_log");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
