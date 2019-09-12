using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CheckingUniqueBoardTitleServerSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Boards",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_Title",
                table: "Boards",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boards_Title",
                table: "Boards");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Boards",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
