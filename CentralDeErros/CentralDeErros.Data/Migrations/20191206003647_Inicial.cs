using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErros.Data.Migrations
{
    public partial class Inicial : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "error_log");
        }
    }
}
