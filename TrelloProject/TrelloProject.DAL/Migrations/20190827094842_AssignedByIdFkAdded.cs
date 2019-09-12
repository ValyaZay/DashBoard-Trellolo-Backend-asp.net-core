using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class AssignedByIdFkAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AssigneeId",
                table: "Cards",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_AssigneeId",
                table: "Cards",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_AssigneeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AssigneeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Cards");
        }
    }
}
