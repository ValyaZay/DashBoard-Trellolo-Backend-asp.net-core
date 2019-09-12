using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CardListsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardLists",
                columns: table => new
                {
                    CardListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardLists", x => x.CardListId);
                    table.ForeignKey(
                        name: "FK_CardLists_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CardLists",
                columns: new[] { "CardListId", "BoardId", "Order", "Title" },
                values: new object[] { 1, 1, 0, "ToDo" });

            migrationBuilder.InsertData(
                table: "CardLists",
                columns: new[] { "CardListId", "BoardId", "Order", "Title" },
                values: new object[] { 2, 1, 1, "Done" });

            migrationBuilder.InsertData(
                table: "CardLists",
                columns: new[] { "CardListId", "BoardId", "Order", "Title" },
                values: new object[] { 3, 1, 2, "DoNOTDo" });

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardLists");
        }
    }
}
