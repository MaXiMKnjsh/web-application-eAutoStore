using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_application_eAutoStore.INFRASTRUCTURE.Migrations
{
    public partial class ChangedImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Vehicles");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Vehicles",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
