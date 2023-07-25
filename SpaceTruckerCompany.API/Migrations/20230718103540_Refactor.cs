using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<double>(type: "float", nullable: false),
                    NumberOfShips = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceShipRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
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
                name: "SpaceShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fuel = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    MaxFuel = table.Column<int>(type: "int", nullable: false),
                    MaxCargo = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceShips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatesX = table.Column<double>(type: "float", nullable: false),
                    CoordinatesY = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceShipEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CargoSpace = table.Column<int>(type: "int", nullable: false),
                    UsedCargoSpace = table.Column<int>(type: "int", nullable: false),
                    CurrentFuel = table.Column<double>(type: "float", nullable: false),
                    FuelUsageRate = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpaceStationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceShipEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceShipEntries_Players_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceShipEntries_SpaceShips_ShipId",
                        column: x => x.ShipId,
                        principalTable: "SpaceShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceShipEntries_SpaceStations_SpaceStationId",
                        column: x => x.SpaceStationId,
                        principalTable: "SpaceStations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TradeItemEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false),
                    SellPrice = table.Column<double>(type: "float", nullable: false),
                    TradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpaceShipId = table.Column<int>(type: "int", nullable: true),
                    SpaceStationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItemEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_SpaceShipEntries_ShipId",
                        column: x => x.ShipId,
                        principalTable: "SpaceShipEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_SpaceShips_SpaceShipId",
                        column: x => x.SpaceShipId,
                        principalTable: "SpaceShips",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_SpaceStations_SpaceStationId",
                        column: x => x.SpaceStationId,
                        principalTable: "SpaceStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_TradeItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "TradeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceShipEntries_OwnerId",
                table: "SpaceShipEntries",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceShipEntries_ShipId",
                table: "SpaceShipEntries",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceShipEntries_SpaceStationId",
                table: "SpaceShipEntries",
                column: "SpaceStationId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_ItemId",
                table: "TradeItemEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_ShipId",
                table: "TradeItemEntries",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_SpaceShipId",
                table: "TradeItemEntries",
                column: "SpaceShipId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_SpaceStationId",
                table: "TradeItemEntries",
                column: "SpaceStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceShipRoutes");

            migrationBuilder.DropTable(
                name: "TradeItemEntries");

            migrationBuilder.DropTable(
                name: "SpaceShipEntries");

            migrationBuilder.DropTable(
                name: "TradeItems");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "SpaceShips");

            migrationBuilder.DropTable(
                name: "SpaceStations");
        }
    }
}
