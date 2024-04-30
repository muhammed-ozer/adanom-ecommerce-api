using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductAttributeOptionsTableAndUpdateProductAttributeAndProductSKURelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductSKUs");

            migrationBuilder.DropTable(
                name: "ProductAttributeOptions");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeOptionId",
                table: "ProductSKUs",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUs_ProductAttributeOptionId",
                table: "ProductSKUs",
                newName: "IX_ProductSKUs_ProductAttributeId");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ProductAttributes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_ProductAttributes_ProductAttributeId",
                table: "ProductSKUs",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_ProductAttributes_ProductAttributeId",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "ProductSKUs",
                newName: "ProductAttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUs_ProductAttributeId",
                table: "ProductSKUs",
                newName: "IX_ProductSKUs_ProductAttributeOptionId");

            migrationBuilder.CreateTable(
                name: "ProductAttributeOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeOptions_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOptions_ProductAttributeId",
                table: "ProductAttributeOptions",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductSKUs",
                column: "ProductAttributeOptionId",
                principalTable: "ProductAttributeOptions",
                principalColumn: "Id");
        }
    }
}
