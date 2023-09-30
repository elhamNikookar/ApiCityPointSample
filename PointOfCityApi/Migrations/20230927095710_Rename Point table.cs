using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfCityApi.Migrations
{
    public partial class RenamePointtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_points_Cities_CityID",
                table: "points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_points",
                table: "points");

            migrationBuilder.RenameTable(
                name: "points",
                newName: "Points");

            migrationBuilder.RenameIndex(
                name: "IX_points_CityID",
                table: "Points",
                newName: "IX_Points_CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "PointID");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Cities_CityID",
                table: "Points",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Cities_CityID",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "points");

            migrationBuilder.RenameIndex(
                name: "IX_Points_CityID",
                table: "points",
                newName: "IX_points_CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_points",
                table: "points",
                column: "PointID");

            migrationBuilder.AddForeignKey(
                name: "FK_points_Cities_CityID",
                table: "points",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
