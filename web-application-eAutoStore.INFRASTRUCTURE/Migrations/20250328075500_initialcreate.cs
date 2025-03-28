using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_application_eAutoStore.INFRASTRUCTURE.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeletedAdvertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOf = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReasonOfRemoving = table.Column<int>(type: "int", nullable: false),
                    RemovingDescrtiption = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedAdvertisements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClosedVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WayOfAttraction = table.Column<int>(type: "int", nullable: false),
                    WayDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WayOfSelling = table.Column<int>(type: "int", nullable: false),
                    Quality = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    DateOfSelling = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedVehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssociatedDeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiringAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatsOfNewAds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOf = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsOfNewAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatsOfNewAds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Quality = table.Column<int>(type: "int", nullable: true),
                    Transmission = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    EngineCapacity = table.Column<float>(type: "real", nullable: true),
                    EnginePower = table.Column<int>(type: "int", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteVehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosedVehicles_UserId",
                table: "ClosedVehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteVehicles_UserId",
                table: "FavoriteVehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteVehicles_VehicleId",
                table: "FavoriteVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsOfNewAds_UserId",
                table: "StatsOfNewAds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedVehicles");

            migrationBuilder.DropTable(
                name: "DeletedAdvertisements");

            migrationBuilder.DropTable(
                name: "FavoriteVehicles");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "StatsOfNewAds");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
