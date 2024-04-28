using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPickUpStoreTableAndRelationsBetweenOrderAndReturnRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingType",
                table: "ShippingSettings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ShippingProviders");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ShippingProviders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "ShippingProviderId",
                table: "ReturnRequests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<byte>(
                name: "DeliveryType",
                table: "ReturnRequests",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "PickUpStoreId",
                table: "ReturnRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShippingProviderId",
                table: "Orders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<byte>(
                name: "DeliveryType",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "PickUpStoreId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PickUpStores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUpStores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequests_PickUpStoreId",
                table: "ReturnRequests",
                column: "PickUpStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickUpStoreId",
                table: "Orders",
                column: "PickUpStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickUpStores_PickUpStoreId",
                table: "Orders",
                column: "PickUpStoreId",
                principalTable: "PickUpStores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnRequests_PickUpStores_PickUpStoreId",
                table: "ReturnRequests",
                column: "PickUpStoreId",
                principalTable: "PickUpStores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickUpStores_PickUpStoreId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnRequests_PickUpStores_PickUpStoreId",
                table: "ReturnRequests");

            migrationBuilder.DropTable(
                name: "PickUpStores");

            migrationBuilder.DropIndex(
                name: "IX_ReturnRequests_PickUpStoreId",
                table: "ReturnRequests");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PickUpStoreId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "PickUpStoreId",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickUpStoreId",
                table: "Orders");

            migrationBuilder.AddColumn<byte>(
                name: "ShippingType",
                table: "ShippingSettings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ShippingProviders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShippingProviderId",
                table: "ReturnRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShippingProviderId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
