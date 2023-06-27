using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    /// <inheritdoc />
    public partial class FixingPlayerCreditsType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Credits",
                table: "Players",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Credits",
                table: "Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
