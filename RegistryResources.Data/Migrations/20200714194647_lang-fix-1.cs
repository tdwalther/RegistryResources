using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryResources.Data.Migrations
{
    public partial class langfix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyState",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Surveys");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionKey",
                table: "Surveys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurveyStateKey",
                table: "Surveys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleKey",
                table: "Surveys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionKey",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyStateKey",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "TitleKey",
                table: "Surveys");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurveyState",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
