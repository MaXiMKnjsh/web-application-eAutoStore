using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_application_eAutoStore.INFRASTRUCTURE.Migrations
{
    public partial class ChangeVehicleBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteVehicles_Users_UserId",
                table: "FavoriteVehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteVehicles_Users_UserId",
                table: "FavoriteVehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteVehicles_Users_UserId",
                table: "FavoriteVehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteVehicles_Users_UserId",
                table: "FavoriteVehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
