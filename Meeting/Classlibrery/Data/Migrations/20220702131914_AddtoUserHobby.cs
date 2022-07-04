using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    public partial class AddtoUserHobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Following",
                table: "UserHobbies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Following",
                table: "UserHobbies");
        }
    }
}
