using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Games_GameId",
                table: "GameTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Tags_TagId",
                table: "GameTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTag",
                table: "GameTag");

            migrationBuilder.RenameTable(
                name: "GameTag",
                newName: "GameTags");

            migrationBuilder.RenameIndex(
                name: "IX_GameTag_TagId",
                table: "GameTags",
                newName: "IX_GameTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTag_GameId",
                table: "GameTags",
                newName: "IX_GameTags_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags",
                columns: new[] { "IdGame", "IdTag" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Games_GameId",
                table: "GameTags",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Tags_TagId",
                table: "GameTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Games_GameId",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Tags_TagId",
                table: "GameTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags");

            migrationBuilder.RenameTable(
                name: "GameTags",
                newName: "GameTag");

            migrationBuilder.RenameIndex(
                name: "IX_GameTags_TagId",
                table: "GameTag",
                newName: "IX_GameTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTags_GameId",
                table: "GameTag",
                newName: "IX_GameTag_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTag",
                table: "GameTag",
                columns: new[] { "IdGame", "IdTag" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameTag_Games_GameId",
                table: "GameTag",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTag_Tags_TagId",
                table: "GameTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
