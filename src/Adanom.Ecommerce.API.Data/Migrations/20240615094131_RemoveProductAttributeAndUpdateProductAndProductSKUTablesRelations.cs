using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductAttributeAndUpdateProductAndProductSKUTablesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousShoppingCartItems_ProductSKUs_ProductSKUId",
                table: "AnonymousShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductSKUs_ProductSKUId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_ProductAttributes_ProductAttributeId",
                table: "ProductSKUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_Products_ProductId",
                table: "ProductSKUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ProductSKUs_ProductSKUId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ProductSKUId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductSKUs_ProductAttributeId",
                table: "ProductSKUs");

            migrationBuilder.DropIndex(
                name: "IX_ProductSKUs_ProductId",
                table: "ProductSKUs");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductSKUId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousShoppingCartItems_ProductSKUId",
                table: "AnonymousShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "StockNotificationItems");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "AnonymousShoppingCartItems");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "ProductSKUId",
                table: "StockNotificationItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductSKUId",
                table: "ShoppingCartItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductAttributeId",
                table: "ProductSKUs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductSKUs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductSKUId",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductSKUId",
                table: "AnonymousShoppingCartItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductSKUId",
                table: "ShoppingCartItems",
                column: "ProductSKUId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSKUs_ProductAttributeId",
                table: "ProductSKUs",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSKUs_ProductId",
                table: "ProductSKUs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductSKUId",
                table: "OrderItems",
                column: "ProductSKUId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousShoppingCartItems_ProductSKUId",
                table: "AnonymousShoppingCartItems",
                column: "ProductSKUId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousShoppingCartItems_ProductSKUs_ProductSKUId",
                table: "AnonymousShoppingCartItems",
                column: "ProductSKUId",
                principalTable: "ProductSKUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductSKUs_ProductSKUId",
                table: "OrderItems",
                column: "ProductSKUId",
                principalTable: "ProductSKUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_ProductAttributes_ProductAttributeId",
                table: "ProductSKUs",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_Products_ProductId",
                table: "ProductSKUs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ProductSKUs_ProductSKUId",
                table: "ShoppingCartItems",
                column: "ProductSKUId",
                principalTable: "ProductSKUs",
                principalColumn: "Id");
        }
    }
}
