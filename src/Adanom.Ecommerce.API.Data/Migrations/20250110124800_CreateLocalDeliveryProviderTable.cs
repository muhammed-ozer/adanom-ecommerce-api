using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateLocalDeliveryProviderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocalDeliveryProviderId",
                table: "AddressDistricts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocalDeliveryProviders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MinimumOrderGrandTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MinimumFreeDeliveryOrderGrandTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FeeTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TaxRate = table.Column<byte>(type: "tinyint", nullable: false),
                    DeliveryInHours = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDeliveryProviders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressDistricts_LocalDeliveryProviderId",
                table: "AddressDistricts",
                column: "LocalDeliveryProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressDistricts_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "AddressDistricts",
                column: "LocalDeliveryProviderId",
                principalTable: "LocalDeliveryProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressDistricts_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "AddressDistricts");

            migrationBuilder.DropTable(
                name: "LocalDeliveryProviders");

            migrationBuilder.DropIndex(
                name: "IX_AddressDistricts_LocalDeliveryProviderId",
                table: "AddressDistricts");

            migrationBuilder.DropColumn(
                name: "LocalDeliveryProviderId",
                table: "AddressDistricts");
        }
    }
}
