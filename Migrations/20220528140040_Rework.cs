using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartBuilding.Migrations
{
    public partial class Rework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeLights");

            migrationBuilder.DropTable(
                name: "HomePresences");

            migrationBuilder.DropTable(
                name: "Homeholders");

            migrationBuilder.CreateTable(
                name: "Humidities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Humidity = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterLevels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    WaterLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterLevels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Humidities");

            migrationBuilder.DropTable(
                name: "WaterLevels");

            migrationBuilder.CreateTable(
                name: "Homeholders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentifyCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeholders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeLights",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TurnedOn = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_HomeLightId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePresences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseholderId = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isPresent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePresences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePresences_Homeholders_HouseholderId",
                        column: x => x.HouseholderId,
                        principalTable: "Homeholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePresences_HouseholderId",
                table: "HomePresences",
                column: "HouseholderId");
        }
    }
}
