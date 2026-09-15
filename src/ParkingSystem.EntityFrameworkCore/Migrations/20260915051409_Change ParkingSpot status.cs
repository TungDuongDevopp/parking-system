using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeParkingSpotstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ParkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "ParkingSpots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
