using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTradeItemEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItemEntries_SpaceShipEntries_SpaceShipEntryId",
                table: "TradeItemEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeItemEntries_SpaceShips_ShipId",
                table: "TradeItemEntries");

            migrationBuilder.RenameColumn(
                name: "SpaceShipEntryId",
                table: "TradeItemEntries",
                newName: "SpaceShipId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItemEntries_SpaceShipEntryId",
                table: "TradeItemEntries",
                newName: "IX_TradeItemEntries_SpaceShipId");

            migrationBuilder.AddColumn<double>(
                name: "BuyPrice",
                table: "TradeItemEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SellPrice",
                table: "TradeItemEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItemEntries_SpaceShipEntries_ShipId",
                table: "TradeItemEntries",
                column: "ShipId",
                principalTable: "SpaceShipEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItemEntries_SpaceShips_SpaceShipId",
                table: "TradeItemEntries",
                column: "SpaceShipId",
                principalTable: "SpaceShips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItemEntries_SpaceShipEntries_ShipId",
                table: "TradeItemEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeItemEntries_SpaceShips_SpaceShipId",
                table: "TradeItemEntries");

            migrationBuilder.DropColumn(
                name: "BuyPrice",
                table: "TradeItemEntries");

            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "TradeItemEntries");

            migrationBuilder.RenameColumn(
                name: "SpaceShipId",
                table: "TradeItemEntries",
                newName: "SpaceShipEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItemEntries_SpaceShipId",
                table: "TradeItemEntries",
                newName: "IX_TradeItemEntries_SpaceShipEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItemEntries_SpaceShipEntries_SpaceShipEntryId",
                table: "TradeItemEntries",
                column: "SpaceShipEntryId",
                principalTable: "SpaceShipEntries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItemEntries_SpaceShips_ShipId",
                table: "TradeItemEntries",
                column: "ShipId",
                principalTable: "SpaceShips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
