using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class UserBoardSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserBoards",
                columns: new[] { "UserId", "BoardId", "UserBoardId" },
                values: new object[] { 2, 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserBoards",
                keyColumns: new[] { "UserId", "BoardId" },
                keyValues: new object[] { 2, 1 });
        }
    }
}
