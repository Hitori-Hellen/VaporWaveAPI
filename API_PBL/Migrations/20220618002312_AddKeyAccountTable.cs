using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_PBL.Migrations
{
    public partial class AddKeyAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Accounts",
                newName: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Accounts",
                newName: "userName");
        }
    }
}
