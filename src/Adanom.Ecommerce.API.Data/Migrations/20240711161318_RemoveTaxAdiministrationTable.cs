using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaxAdiministrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingAddresses_TaxAdministrations_TaxAdministrationId",
                table: "BillingAddresses");

            migrationBuilder.DropTable(
                name: "TaxAdministrations");

            migrationBuilder.DropIndex(
                name: "IX_BillingAddresses_TaxAdministrationId",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "TaxAdministrationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TaxAdministrationId",
                table: "BillingAddresses");

            migrationBuilder.AddColumn<string>(
                name: "TaxAdministration",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxAdministration",
                table: "BillingAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxAdministration",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TaxAdministration",
                table: "BillingAddresses");

            migrationBuilder.AddColumn<long>(
                name: "TaxAdministrationId",
                table: "Companies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxAdministrationId",
                table: "BillingAddresses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TaxAdministrations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxAdministrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddresses_TaxAdministrationId",
                table: "BillingAddresses",
                column: "TaxAdministrationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxAdministrations_Code",
                table: "TaxAdministrations",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAddresses_TaxAdministrations_TaxAdministrationId",
                table: "BillingAddresses",
                column: "TaxAdministrationId",
                principalTable: "TaxAdministrations",
                principalColumn: "Id");
        }
    }
}
