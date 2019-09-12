using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CreatedByIdFkAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cards",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                column: "CreatedById",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                column: "CreatedById",
                value: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 2, "vova@vova.com", "Vova", "Petrov" },
                    { 3, "gora@gora.net", "Gora", "Sidorov" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CreatedById",
                table: "Cards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_CreatedById",
                table: "Cards",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_CreatedById",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CreatedById",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "Assignee",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cards",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                columns: new[] { "Assignee", "CreatedBy" },
                values: new object[] { "Vova", "Valya" });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                columns: new[] { "Assignee", "CreatedBy" },
                values: new object[] { "Vova", "Gora" });
        }
    }
}
