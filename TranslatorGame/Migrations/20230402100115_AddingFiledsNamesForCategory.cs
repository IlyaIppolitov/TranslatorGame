using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslatorGame.Migrations
{
    public partial class AddingFiledsNamesForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "RussianName");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GermanName",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "GermanName",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "RussianName",
                table: "Categories",
                newName: "Name");
        }
    }
}
