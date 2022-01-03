using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevGames.API.Persistence.Migrations
{
    public partial class AddUserInEntitiePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Posts");
        }
    }
}
