using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FilterisDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleCode",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_Email",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_PhoneNumber",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_VehicleType_Duration_DurationUnit",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_ParkingAreaId_SpotCode",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingAreas_ParkingCode",
                table: "ParkingAreas");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCode",
                table: "Vehicles",
                column: "VehicleCode",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Email",
                table: "Staffs",
                column: "Email",
                unique: true,
                filter: "[IsDeleted] = 0 AND [Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_PhoneNumber",
                table: "Staffs",
                column: "PhoneNumber",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_VehicleType_Duration_DurationUnit",
                table: "Quotations",
                columns: new[] { "VehicleType", "Duration", "DurationUnit" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_ParkingAreaId_SpotCode",
                table: "ParkingSpots",
                columns: new[] { "ParkingAreaId", "SpotCode" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingAreas_ParkingCode",
                table: "ParkingAreas",
                column: "ParkingCode",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[IsDeleted] = 0 AND [Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleCode",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_Email",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_PhoneNumber",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_VehicleType_Duration_DurationUnit",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_ParkingAreaId_SpotCode",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingAreas_ParkingCode",
                table: "ParkingAreas");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCode",
                table: "Vehicles",
                column: "VehicleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Email",
                table: "Staffs",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_PhoneNumber",
                table: "Staffs",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_VehicleType_Duration_DurationUnit",
                table: "Quotations",
                columns: new[] { "VehicleType", "Duration", "DurationUnit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_ParkingAreaId_SpotCode",
                table: "ParkingSpots",
                columns: new[] { "ParkingAreaId", "SpotCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingAreas_ParkingCode",
                table: "ParkingAreas",
                column: "ParkingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
