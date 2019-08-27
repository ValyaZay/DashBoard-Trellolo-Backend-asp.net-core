using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CardsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Assignee = table.Column<string>(nullable: true),
                    Hidden = table.Column<bool>(nullable: false),
                    CardListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_CardLists_CardListId",
                        column: x => x.CardListId,
                        principalTable: "CardLists",
                        principalColumn: "CardListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "Assignee", "CardListId", "CreatedBy", "CreatedDate", "Description", "Hidden", "Title" },
                values: new object[] { 1, "Vova", 1, "Valya", new DateTime(2019, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just create a new task", false, "Create a task" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "Assignee", "CardListId", "CreatedBy", "CreatedDate", "Description", "Hidden", "Title" },
                values: new object[] { 2, "Vova", 1, "Gora", new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Implement INewInterface now", false, "Implement an Interface" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListId",
                table: "Cards",
                column: "CardListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
