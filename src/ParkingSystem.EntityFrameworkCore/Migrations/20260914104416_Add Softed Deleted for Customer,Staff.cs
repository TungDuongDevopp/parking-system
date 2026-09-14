using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftedDeletedforCustomerStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Staffs_UserId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserId",
                table: "Staffs",
                column: "UserId",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Staffs_UserId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserId",
                table: "Staffs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);
        }
    }
}
