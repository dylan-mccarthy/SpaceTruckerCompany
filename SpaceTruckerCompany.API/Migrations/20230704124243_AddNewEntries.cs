using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "SpaceShips");

            migrationBuilder.RenameColumn(
                name: "Credits",
                table: "SpaceShipEntries",
                newName: "FuelUsageRate");

            migrationBuilder.AddColumn<string>(
                name: "TradeType",
                table: "TradeItemEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "CurrentFuel",
                table: "SpaceShipEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SpaceShipEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeType",
                table: "TradeItemEntries");

            migrationBuilder.DropColumn(
                name: "CurrentFuel",
                table: "SpaceShipEntries");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "SpaceShipEntries");

            migrationBuilder.RenameColumn(
                name: "FuelUsageRate",
                table: "SpaceShipEntries",
                newName: "Credits");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SpaceShips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
