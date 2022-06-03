using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class FourMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Games_GameId",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Tags_TagId",
                table: "GameTags");

            migrationBuilder.DropIndex(
                name: "IX_GameTags_GameId",
                table: "GameTags");

            migrationBuilder.DropIndex(
                name: "IX_GameTags_TagId",
                table: "GameTags");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "GameTags");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GameId",
                table: "Tags",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTags_IdTag",
                table: "GameTags",
                column: "IdTag");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Games_IdGame",
                table: "GameTags",
                column: "IdGame",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Tags_IdTag",
                table: "GameTags",
                column: "IdTag",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Games_GameId",
                table: "Tags",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Games_IdGame",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Tags_IdTag",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Games_GameId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GameId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_GameTags_IdTag",
                table: "GameTags");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "GameTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameTags_GameId",
                table: "GameTags",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTags_TagId",
                table: "GameTags",
                column: "TagId");

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
    }
}
