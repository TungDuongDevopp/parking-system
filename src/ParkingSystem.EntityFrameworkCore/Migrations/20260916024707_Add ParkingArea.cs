using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddParkingArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ParkingSpotId",
                table: "ParkingSessions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ParkingAreaId",
                table: "ParkingSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_ParkingAreaId",
                table: "ParkingSessions",
                column: "ParkingAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSessions_ParkingAreas_ParkingAreaId",
                table: "ParkingSessions",
                column: "ParkingAreaId",
                principalTable: "ParkingAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSessions_ParkingAreas_ParkingAreaId",
                table: "ParkingSessions");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSessions_ParkingAreaId",
                table: "ParkingSessions");

            migrationBuilder.DropColumn(
                name: "ParkingAreaId",
                table: "ParkingSessions");

            migrationBuilder.AlterColumn<long>(
                name: "ParkingSpotId",
                table: "ParkingSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
