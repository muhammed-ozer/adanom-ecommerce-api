using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductTaxTableNameToTaxCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_ProductTaxes_ProductTaxId",
                table: "ProductPrices");

            migrationBuilder.DropTable(
                name: "ProductTaxes");

            migrationBuilder.RenameColumn(
                name: "ProductTaxId",
                table: "ProductPrices",
                newName: "TaxCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPrices_ProductTaxId",
                table: "ProductPrices",
                newName: "IX_ProductPrices_TaxCategoryId");

            migrationBuilder.CreateTable(
                name: "TaxCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rate = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_TaxCategories_TaxCategoryId",
                table: "ProductPrices",
                column: "TaxCategoryId",
                principalTable: "TaxCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_TaxCategories_TaxCategoryId",
                table: "ProductPrices");

            migrationBuilder.DropTable(
                name: "TaxCategories");

            migrationBuilder.RenameColumn(
                name: "TaxCategoryId",
                table: "ProductPrices",
                newName: "ProductTaxId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPrices_TaxCategoryId",
                table: "ProductPrices",
                newName: "IX_ProductPrices_ProductTaxId");

            migrationBuilder.CreateTable(
                name: "ProductTaxes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rate = table.Column<byte>(type: "tinyint", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTaxes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_ProductTaxes_ProductTaxId",
                table: "ProductPrices",
                column: "ProductTaxId",
                principalTable: "ProductTaxes",
                principalColumn: "Id");
        }
    }
}
