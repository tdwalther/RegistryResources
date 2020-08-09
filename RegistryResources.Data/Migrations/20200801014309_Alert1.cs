using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryResources.Data.Migrations
{
    public partial class Alert1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DisplayDate = table.Column<DateTime>(nullable: false),
                    ReplayCount = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Display = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");
        }
    }
}
