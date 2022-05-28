using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartBuilding.Migrations
{
    public partial class Rework2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterLevels",
                table: "WaterLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Humidities",
                table: "Humidities");

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_HomeWaterLevelId",
                table: "WaterLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_HomeHumidityId",
                table: "Humidities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_HomeWaterLevelId",
                table: "WaterLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_HomeHumidityId",
                table: "Humidities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterLevels",
                table: "WaterLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Humidities",
                table: "Humidities",
                column: "Id");
        }
    }
}
