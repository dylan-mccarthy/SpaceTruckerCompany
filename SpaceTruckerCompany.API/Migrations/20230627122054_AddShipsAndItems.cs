using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class AddShipsAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceShips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShipId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CargoSpace = table.Column<int>(type: "int", nullable: false),
                    UsedCargoSpace = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<double>(type: "float", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TradeItemEntries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShipId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SpaceShipEntryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItemEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_SpaceShipEntries_SpaceShipEntryId",
                        column: x => x.SpaceShipEntryId,
                        principalTable: "SpaceShipEntries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_SpaceShips_ShipId",
                        column: x => x.ShipId,
                        principalTable: "SpaceShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeItemEntries_TradeItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "TradeItems",
                        principalColumn: "Id");
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
                name: "IX_TradeItemEntries_ItemId",
                table: "TradeItemEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_ShipId",
                table: "TradeItemEntries",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItemEntries_SpaceShipEntryId",
                table: "TradeItemEntries",
                column: "SpaceShipEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeItemEntries");

            migrationBuilder.DropTable(
                name: "SpaceShipEntries");

            migrationBuilder.DropTable(
                name: "TradeItems");

            migrationBuilder.DropTable(
                name: "SpaceShips");
        }
    }
}
