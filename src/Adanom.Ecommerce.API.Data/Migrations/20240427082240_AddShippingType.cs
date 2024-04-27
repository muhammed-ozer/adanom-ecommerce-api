using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<long>(
                name: "ShippingSettingsId",
                table: "ShippingProviders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "ShippingTransactionCode",
                table: "ReturnRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingProviders_ShippingSettings_ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.DropIndex(
                name: "IX_ShippingProviders_ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "ShippingType",
                table: "ShippingSettings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "ShippingSettingsId",
                table: "ShippingProviders");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingTransactionCode",
                table: "ReturnRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
