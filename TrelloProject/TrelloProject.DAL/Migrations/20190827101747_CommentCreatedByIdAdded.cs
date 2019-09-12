using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CommentCreatedByIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CardComments");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "CardComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CardComments",
                keyColumn: "CardCommentId",
                keyValue: 1,
                column: "CreatedById",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CardComments",
                keyColumn: "CardCommentId",
                keyValue: 2,
                column: "CreatedById",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                column: "AssigneeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                column: "AssigneeId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_CreatedById",
                table: "CardComments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CardComments_Users_CreatedById",
                table: "CardComments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardComments_Users_CreatedById",
                table: "CardComments");

            migrationBuilder.DropIndex(
                name: "IX_CardComments_CreatedById",
                table: "CardComments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CardComments");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CardComments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CardComments",
                keyColumn: "CardCommentId",
                keyValue: 1,
                column: "CreatedBy",
                value: "Sasha");

            migrationBuilder.UpdateData(
                table: "CardComments",
                keyColumn: "CardCommentId",
                keyValue: 2,
                column: "CreatedBy",
                value: "Kate");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                column: "AssigneeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                column: "AssigneeId",
                value: null);
        }
    }
}
