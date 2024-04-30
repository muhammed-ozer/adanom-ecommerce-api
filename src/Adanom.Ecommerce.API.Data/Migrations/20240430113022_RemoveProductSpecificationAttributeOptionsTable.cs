using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductSpecificationAttributeOptionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_ProductSpecificationAttributeOption_Mappings");

            migrationBuilder.DropTable(
                name: "ProductSpecificationAttributeOptions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSpecificationAttributes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ProductSpecificationAttributes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Product_ProductSpecificationAttribute_Mappings",
                columns: table => new
                {
                    ProductSpecificationAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductSpecificationAttribute_Mappings", x => new { x.ProductId, x.ProductSpecificationAttributeId });
                    table.ForeignKey(
                        name: "FK_Product_ProductSpecificationAttribute_Mappings_ProductSpecificationAttributes_ProductSpecificationAttributeId",
                        column: x => x.ProductSpecificationAttributeId,
                        principalTable: "ProductSpecificationAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductSpecificationAttribute_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSpecificationAttribute_Mappings_ProductSpecificationAttributeId_ProductId",
                table: "Product_ProductSpecificationAttribute_Mappings",
                columns: new[] { "ProductSpecificationAttributeId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_ProductSpecificationAttribute_Mappings");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductSpecificationAttributes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSpecificationAttributes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateTable(
                name: "ProductSpecificationAttributeOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSpecificationAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationAttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationAttributeOptions_ProductSpecificationAttributes_ProductSpecificationAttributeId",
                        column: x => x.ProductSpecificationAttributeId,
                        principalTable: "ProductSpecificationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductSpecificationAttributeOption_Mappings",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductSpecificationAttributeOptionId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductSpecificationAttributeOption_Mappings", x => new { x.ProductId, x.ProductSpecificationAttributeOptionId });
                    table.ForeignKey(
                        name: "FK_Product_ProductSpecificationAttributeOption_Mappings_ProductSpecificationAttributeOptions_ProductSpecificationAttributeOptio~",
                        column: x => x.ProductSpecificationAttributeOptionId,
                        principalTable: "ProductSpecificationAttributeOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductSpecificationAttributeOption_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSpecificationAttributeOption_Mappings_ProductSpecificationAttributeOptionId_ProductId",
                table: "Product_ProductSpecificationAttributeOption_Mappings",
                columns: new[] { "ProductSpecificationAttributeOptionId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationAttributeOptions_ProductSpecificationAttributeId",
                table: "ProductSpecificationAttributeOptions",
                column: "ProductSpecificationAttributeId");
        }
    }
}
