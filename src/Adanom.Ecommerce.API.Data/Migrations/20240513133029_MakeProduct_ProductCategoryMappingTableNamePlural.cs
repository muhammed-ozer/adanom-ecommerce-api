using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeProduct_ProductCategoryMappingTableNamePlural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_Mapping_ProductCategories_ProductCategoryId",
                table: "Product_ProductCategory_Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_Mapping_Products_ProductId",
                table: "Product_ProductCategory_Mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_ProductCategory_Mapping",
                table: "Product_ProductCategory_Mapping");

            migrationBuilder.RenameTable(
                name: "Product_ProductCategory_Mapping",
                newName: "Product_ProductCategory_Mappings");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategory_Mapping_ProductId",
                table: "Product_ProductCategory_Mappings",
                newName: "IX_Product_ProductCategory_Mappings_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategory_Mapping_ProductCategoryId_ProductId",
                table: "Product_ProductCategory_Mappings",
                newName: "IX_Product_ProductCategory_Mappings_ProductCategoryId_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_ProductCategory_Mappings",
                table: "Product_ProductCategory_Mappings",
                columns: new[] { "ProductCategoryId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_Mappings_ProductCategories_ProductCategoryId",
                table: "Product_ProductCategory_Mappings",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_Mappings_Products_ProductId",
                table: "Product_ProductCategory_Mappings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_Mappings_ProductCategories_ProductCategoryId",
                table: "Product_ProductCategory_Mappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_Mappings_Products_ProductId",
                table: "Product_ProductCategory_Mappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_ProductCategory_Mappings",
                table: "Product_ProductCategory_Mappings");

            migrationBuilder.RenameTable(
                name: "Product_ProductCategory_Mappings",
                newName: "Product_ProductCategory_Mapping");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategory_Mappings_ProductId",
                table: "Product_ProductCategory_Mapping",
                newName: "IX_Product_ProductCategory_Mapping_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategory_Mappings_ProductCategoryId_ProductId",
                table: "Product_ProductCategory_Mapping",
                newName: "IX_Product_ProductCategory_Mapping_ProductCategoryId_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_ProductCategory_Mapping",
                table: "Product_ProductCategory_Mapping",
                columns: new[] { "ProductCategoryId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_Mapping_ProductCategories_ProductCategoryId",
                table: "Product_ProductCategory_Mapping",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_Mapping_Products_ProductId",
                table: "Product_ProductCategory_Mapping",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
