using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackgroundColors",
                columns: table => new
                {
                    BackgroundColorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorHex = table.Column<string>(nullable: true),
                    ColorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundColors", x => x.BackgroundColorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    BoardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CurrentBackgroundColorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.BoardId);
                    table.ForeignKey(
                        name: "FK_Boards_BackgroundColors_CurrentBackgroundColorId",
                        column: x => x.CurrentBackgroundColorId,
                        principalTable: "BackgroundColors",
                        principalColumn: "BackgroundColorId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserBoards",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoards", x => new { x.UserId, x.BoardId });
                    table.ForeignKey(
                        name: "FK_UserBoards_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBoards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    CardListId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cards_CardLists_CardListId",
                        column: x => x.CardListId,
                        principalTable: "CardLists",
                        principalColumn: "CardListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardComments",
                columns: table => new
                {
                    CardCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    RefersToId = table.Column<int>(nullable: true),
                    CardId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false)
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
                        name: "FK_CardComments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardComments_CardComments_RefersToId",
                        column: x => x.RefersToId,
                        principalTable: "CardComments",
                        principalColumn: "CardCommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BackgroundColors",
                columns: new[] { "BackgroundColorId", "ColorHex", "ColorName" },
                values: new object[,]
                {
                    { 1, "#C0C0C0", "Grey" },
                    { 2, "#ffff00", "Yellow" },
                    { 3, "#FFA500", "Orange" },
                    { 4, "#0000FF", "Blue" },
                    { 5, "#008000", "Green" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "valya@valya.net", "Valya", "Zay" },
                    { 2, "vova@vova.com", "Vova", "Petrov" },
                    { 3, "gora@gora.net", "Gora", "Sidorov" }
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "BoardId", "CurrentBackgroundColorId", "Title" },
                values: new object[] { 1, 1, "ManagerBoard" });

            migrationBuilder.InsertData(
                table: "CardLists",
                columns: new[] { "CardListId", "BoardId", "Order", "Title" },
                values: new object[,]
                {
                    { 1, 1, 0, "ToDo" },
                    { 2, 1, 1, "Done" },
                    { 3, 1, 2, "DoNOTDo" }
                });

            migrationBuilder.InsertData(
                table: "UserBoards",
                columns: new[] { "UserId", "BoardId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "AssigneeId", "CardListId", "CreatedById", "CreatedDate", "Description", "Hidden", "Title" },
                values: new object[] { 1, 2, 1, 1, new DateTime(2019, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just create a new task", false, "Create a task" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "AssigneeId", "CardListId", "CreatedById", "CreatedDate", "Description", "Hidden", "Title" },
                values: new object[] { 2, 3, 1, 1, new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Implement INewInterface now", false, "Implement an Interface" });

            migrationBuilder.InsertData(
                table: "CardComments",
                columns: new[] { "CardCommentId", "CardId", "CreatedById", "CreatedDate", "RefersToId", "Text" },
                values: new object[] { 1, 1, 2, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Good comment" });

            migrationBuilder.InsertData(
                table: "CardComments",
                columns: new[] { "CardCommentId", "CardId", "CreatedById", "CreatedDate", "RefersToId", "Text" },
                values: new object[] { 2, 1, 2, new DateTime(2019, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bad comment" });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CurrentBackgroundColorId",
                table: "Boards",
                column: "CurrentBackgroundColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_Title",
                table: "Boards",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_CardId",
                table: "CardComments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_CreatedById",
                table: "CardComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_RefersToId",
                table: "CardComments",
                column: "RefersToId");

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AssigneeId",
                table: "Cards",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListId",
                table: "Cards",
                column: "CardListId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CreatedById",
                table: "Cards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoards_BoardId",
                table: "UserBoards",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardComments");

            migrationBuilder.DropTable(
                name: "UserBoards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CardLists");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "BackgroundColors");
        }
    }
}
