using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePriceColumnNameToTaxExcludedPriceOnItemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ReturnRequestItems",
                newName: "TaxExcludedPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "TaxExcludedPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxExcludedPrice",
                table: "ReturnRequestItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TaxExcludedPrice",
                table: "OrderItems",
                newName: "Price");
        }
    }
}
