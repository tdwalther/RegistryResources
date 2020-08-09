using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryResources.Data.Migrations
{
    public partial class AddedUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Registrants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Registrants");
        }
    }
}
