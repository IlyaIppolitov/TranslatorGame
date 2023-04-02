using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslatorGame.Migrations
{
    public partial class WordPropertiesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RussianText",
                table: "Words",
                newName: "Russian");

            migrationBuilder.RenameColumn(
                name: "GermanText",
                table: "Words",
                newName: "German");

            migrationBuilder.RenameColumn(
                name: "EnglishText",
                table: "Words",
                newName: "English");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Russian",
                table: "Words",
                newName: "RussianText");

            migrationBuilder.RenameColumn(
                name: "German",
                table: "Words",
                newName: "GermanText");

            migrationBuilder.RenameColumn(
                name: "English",
                table: "Words",
                newName: "EnglishText");
        }
    }
}
