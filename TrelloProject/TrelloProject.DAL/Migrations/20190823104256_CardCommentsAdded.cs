using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class CardCommentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardComments",
                columns: table => new
                {
                    CardCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    RefersToId = table.Column<int>(nullable: true),
                    CardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardComments", x => x.CardCommentId);
                    table.ForeignKey(
                        name: "FK_CardComments_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardComments_CardComments_RefersToId",
                        column: x => x.RefersToId,
                        principalTable: "CardComments",
                        principalColumn: "CardCommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CardComments",
                columns: new[] { "CardCommentId", "CardId", "CreatedBy", "CreatedDate", "RefersToId", "Text" },
                values: new object[] { 1, 1, "Sasha", new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Good comment" });

            migrationBuilder.InsertData(
                table: "CardComments",
                columns: new[] { "CardCommentId", "CardId", "CreatedBy", "CreatedDate", "RefersToId", "Text" },
                values: new object[] { 2, 1, "Kate", new DateTime(2019, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bad comment" });

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_CardId",
                table: "CardComments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_RefersToId",
                table: "CardComments",
                column: "RefersToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardComments");
        }
    }
}
