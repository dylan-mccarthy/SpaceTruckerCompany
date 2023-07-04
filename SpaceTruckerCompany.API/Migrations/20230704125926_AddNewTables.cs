using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpaceStationId",
                table: "TradeItemEntries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpaceStationId",
                table: "SpaceShipEntries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpaceShipRoutes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentCoordinatesX = table.Column<double>(type: "float", nullable: false),
                    CurrentCoordinatesY = table.Column<double>(type: "float", nullable: false),
                    DestinationCoordinatesX = table.Column<double>(type: "float", nullable: false),
                    DestinationCoordinatesY = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceShipRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceStations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatesX = table.Column<double>(type: "float", nullable: false),
                    CoordinatesY = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceStations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_SpaceStationId",
                table: "TradeItemEntries",
                column: "SpaceStationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceShipEntries_SpaceStationId",
                table: "SpaceShipEntries",
                column: "SpaceStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceShipEntries_SpaceStations_SpaceStationId",
                table: "SpaceShipEntries",
                column: "SpaceStationId",
                principalTable: "SpaceStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItemEntries_SpaceStations_SpaceStationId",
                table: "TradeItemEntries",
                column: "SpaceStationId",
                principalTable: "SpaceStations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceShipEntries_SpaceStations_SpaceStationId",
                table: "SpaceShipEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeItemEntries_SpaceStations_SpaceStationId",
                table: "TradeItemEntries");

            migrationBuilder.DropTable(
                name: "SpaceShipRoutes");

            migrationBuilder.DropTable(
                name: "SpaceStations");

            migrationBuilder.DropIndex(
                name: "IX_TradeItemEntries_SpaceStationId",
                table: "TradeItemEntries");

            migrationBuilder.DropIndex(
                name: "IX_SpaceShipEntries_SpaceStationId",
                table: "SpaceShipEntries");

            migrationBuilder.DropColumn(
                name: "SpaceStationId",
                table: "TradeItemEntries");

            migrationBuilder.DropColumn(
                name: "SpaceStationId",
                table: "SpaceShipEntries");
        }
    }
}
