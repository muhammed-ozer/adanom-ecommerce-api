using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShippingSettingsTableAndAddItsPropsToShippingProviderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingProviders_ShippingSettings_ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.DropTable(
                name: "ShippingSettings");

            migrationBuilder.DropIndex(
                name: "IX_ShippingProviders_ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.AddColumn<decimal>(
                name: "FeeTax",
                table: "ShippingProviders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeTotal",
                table: "ShippingProviders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeWithoutTax",
                table: "ShippingProviders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumFreeShippingTotalPrice",
                table: "ShippingProviders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "ShippingInDays",
                table: "ShippingProviders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "TaxRate",
                table: "ShippingProviders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeTax",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "FeeTotal",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "FeeWithoutTax",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "MinimumFreeShippingTotalPrice",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "ShippingInDays",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ShippingProviders");

            migrationBuilder.AddColumn<long>(
                name: "ShippingSettingsId",
                table: "ShippingProviders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ShippingSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeTax = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FeeTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FeeWithoutTax = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MinimumFreeShippingTotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ShippingInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    TaxRate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviders_ShippingSettingsId",
                table: "ShippingProviders",
                column: "ShippingSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingProviders_ShippingSettings_ShippingSettingsId",
                table: "ShippingProviders",
                column: "ShippingSettingsId",
                principalTable: "ShippingSettings",
                principalColumn: "Id");
        }
    }
}
