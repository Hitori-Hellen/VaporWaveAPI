using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class addIdLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Library",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Library_userId",
                table: "Library",
                column: "userId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.DropIndex(
                name: "IX_Library_userId",
                table: "Library");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Library");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "userId");
        }
    }
}
