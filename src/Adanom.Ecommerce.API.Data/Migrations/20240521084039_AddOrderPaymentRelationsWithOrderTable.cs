using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderPaymentRelationsWithOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_OrderId",
                table: "OrderPayments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Orders_OrderId",
                table: "OrderPayments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Orders_OrderId",
                table: "OrderPayments");

            migrationBuilder.DropIndex(
                name: "IX_OrderPayments_OrderId",
                table: "OrderPayments");
        }
    }
}
