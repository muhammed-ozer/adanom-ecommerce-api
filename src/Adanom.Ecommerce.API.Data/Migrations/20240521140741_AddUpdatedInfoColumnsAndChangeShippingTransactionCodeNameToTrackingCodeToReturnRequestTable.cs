using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedInfoColumnsAndChangeShippingTransactionCodeNameToTrackingCodeToReturnRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingTransactionCode",
                table: "ReturnRequests",
                newName: "ShippingTrackingCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "ReturnRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                table: "ReturnRequests",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "ReturnRequests");

            migrationBuilder.RenameColumn(
                name: "ShippingTrackingCode",
                table: "ReturnRequests",
                newName: "ShippingTransactionCode");
        }
    }
}
