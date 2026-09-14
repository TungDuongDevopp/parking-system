using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class LicensePlateUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicensePlate",
                table: "Vehicles",
                column: "LicensePlate",
                unique: true,
                filter: "[IsDeleted] = 0 AND [LicensePlate] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_LicensePlate",
                table: "Vehicles");
        }
    }
}
