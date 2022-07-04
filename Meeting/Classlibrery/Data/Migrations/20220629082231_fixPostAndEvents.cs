using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    public partial class fixPostAndEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Cenceld",
                table: "Events",
                newName: "Canceled");

            migrationBuilder.AddColumn<string>(
                name: "CreatorPhotoUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorPhotoUrl",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Canceled",
                table: "Events",
                newName: "Cenceld");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
