using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class RemovingUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
