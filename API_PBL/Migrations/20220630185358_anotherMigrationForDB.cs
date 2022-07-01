using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class anotherMigrationForDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_userId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Library_userId",
                table: "Library");

            migrationBuilder.CreateIndex(
                name: "IX_Library_userId",
                table: "Library",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Library_userId",
                table: "Library");

            migrationBuilder.CreateIndex(
                name: "IX_Library_userId",
                table: "Library",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_userId",
                table: "WishLists",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
