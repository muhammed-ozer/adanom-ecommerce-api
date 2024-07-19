using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalPriceAndDiscountedPriceCOlumnsToShoppingCartItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ShoppingCartItems",
                newName: "OriginalPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "ShoppingCartItems",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "OriginalPrice",
                table: "ShoppingCartItems",
                newName: "Price");
        }
    }
}
