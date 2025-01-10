using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMinimumTotalColumnsToShippingProviderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeTax",
                table: "ShippingProviders");

            migrationBuilder.RenameColumn(
                name: "MinimumFreeShippingTotalPrice",
                table: "ShippingProviders",
                newName: "MinimumOrderGrandTotal");

            migrationBuilder.RenameColumn(
                name: "FeeWithoutTax",
                table: "ShippingProviders",
                newName: "MinimumFreeShippingOrderGrandTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumOrderGrandTotal",
                table: "ShippingProviders",
                newName: "MinimumFreeShippingTotalPrice");

            migrationBuilder.RenameColumn(
                name: "MinimumFreeShippingOrderGrandTotal",
                table: "ShippingProviders",
                newName: "FeeWithoutTax");

            migrationBuilder.AddColumn<decimal>(
                name: "FeeTax",
                table: "ShippingProviders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
