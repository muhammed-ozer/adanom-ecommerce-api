using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdColumnsOnMappingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product_ProductTag_Mappings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product_ProductSpecificationAttribute_Mappings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product_ProductCategory_Mapping");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Product_ProductTag_Mappings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Product_ProductSpecificationAttribute_Mappings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Product_ProductCategory_Mapping",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
