using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartBuilding.Migrations
{
    public partial class xa2fx2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homeholders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IdentifyCode = table.Column<string>(type: "TEXT", nullable: false)
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
                    TurnedOn = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_HomeLightId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_HomeTemperatureId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePresences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HouseholderId = table.Column<int>(type: "INTEGER", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeLights");

            migrationBuilder.DropTable(
                name: "HomePresences");

            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "Homeholders");
        }
    }
}
