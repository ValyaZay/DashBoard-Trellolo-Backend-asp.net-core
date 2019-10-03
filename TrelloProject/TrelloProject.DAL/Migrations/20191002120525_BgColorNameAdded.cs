using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class BgColorNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "BackgroundColors",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BackgroundColors",
                keyColumn: "BackgroundColorId",
                keyValue: 1,
                column: "ColorName",
                value: "Grey");

            migrationBuilder.UpdateData(
                table: "BackgroundColors",
                keyColumn: "BackgroundColorId",
                keyValue: 2,
                column: "ColorName",
                value: "Yellow");

            migrationBuilder.UpdateData(
                table: "BackgroundColors",
                keyColumn: "BackgroundColorId",
                keyValue: 3,
                column: "ColorName",
                value: "Orange");

            migrationBuilder.UpdateData(
                table: "BackgroundColors",
                keyColumn: "BackgroundColorId",
                keyValue: 4,
                column: "ColorName",
                value: "Blue");

            migrationBuilder.UpdateData(
                table: "BackgroundColors",
                keyColumn: "BackgroundColorId",
                keyValue: 5,
                column: "ColorName",
                value: "Green");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "BackgroundColors");
        }
    }
}
