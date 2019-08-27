using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloProject.DAL.Migrations
{
    public partial class BackgroundColorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentBackgroundColorId",
                table: "Boards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BackgroundColors",
                columns: table => new
                {
                    BackgroundColorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorHex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundColors", x => x.BackgroundColorId);
                });

            migrationBuilder.InsertData(
                table: "BackgroundColors",
                columns: new[] { "BackgroundColorId", "ColorHex" },
                values: new object[,]
                {
                    { 1, "#C0C0C0" },
                    { 2, "#ffff00" },
                    { 3, "#FFA500" },
                    { 4, "#0000FF" },
                    { 5, "#008000" }
                });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "BoardId",
                keyValue: 1,
                column: "CurrentBackgroundColorId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CurrentBackgroundColorId",
                table: "Boards",
                column: "CurrentBackgroundColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_BackgroundColors_CurrentBackgroundColorId",
                table: "Boards",
                column: "CurrentBackgroundColorId",
                principalTable: "BackgroundColors",
                principalColumn: "BackgroundColorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_BackgroundColors_CurrentBackgroundColorId",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "BackgroundColors");

            migrationBuilder.DropIndex(
                name: "IX_Boards_CurrentBackgroundColorId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CurrentBackgroundColorId",
                table: "Boards");
        }
    }
}
