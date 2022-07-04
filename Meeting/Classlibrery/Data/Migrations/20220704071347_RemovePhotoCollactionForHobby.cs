using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    public partial class RemovePhotoCollactionForHobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhotosHobby_EntityId",
                table: "PhotosHobby");

            migrationBuilder.AddColumn<int>(
                name: "photoId",
                table: "Hobbies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhotosHobby_EntityId",
                table: "PhotosHobby",
                column: "EntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhotosHobby_EntityId",
                table: "PhotosHobby");

            migrationBuilder.DropColumn(
                name: "photoId",
                table: "Hobbies");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosHobby_EntityId",
                table: "PhotosHobby",
                column: "EntityId");
        }
    }
}
