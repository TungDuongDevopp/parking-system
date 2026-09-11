using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TransactionCode",
                table: "PaymentTransactions",
                column: "TransactionCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");
        }
    }
}
