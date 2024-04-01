using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_application_eAutoStore.INFRASTRUCTURE.Migrations
{
    public partial class addedPhotoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Vehicles",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Vehicles");
        }
    }
}
