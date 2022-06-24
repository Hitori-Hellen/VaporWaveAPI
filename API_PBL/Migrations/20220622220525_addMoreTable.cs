using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class addMoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLiked",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gameName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Library_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gameName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.userId);
                    table.ForeignKey(
                        name: "FK_WishLists_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Library");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropColumn(
                name: "isLiked",
                table: "Games");
        }
    }
}
