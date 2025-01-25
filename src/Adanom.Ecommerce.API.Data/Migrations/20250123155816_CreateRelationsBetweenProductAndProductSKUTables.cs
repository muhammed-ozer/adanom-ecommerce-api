using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationsBetweenProductAndProductSKUTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSKUs_ProductSKUId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSKUId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Product_ProductSKU_Mappings",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductSKUId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductSKU_Mappings", x => new { x.ProductId, x.ProductSKUId });
                    table.ForeignKey(
                        name: "FK_Product_ProductSKU_Mappings_ProductSKUs_ProductSKUId",
                        column: x => x.ProductSKUId,
                        principalTable: "ProductSKUs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductSKU_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSKU_Mappings_ProductSKUId_ProductId",
                table: "Product_ProductSKU_Mappings",
                columns: new[] { "ProductSKUId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_ProductSKU_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "ProductSKUId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSKUId",
                table: "Products",
                column: "ProductSKUId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSKUs_ProductSKUId",
                table: "Products",
                column: "ProductSKUId",
                principalTable: "ProductSKUs",
                principalColumn: "Id");
        }
    }
}
