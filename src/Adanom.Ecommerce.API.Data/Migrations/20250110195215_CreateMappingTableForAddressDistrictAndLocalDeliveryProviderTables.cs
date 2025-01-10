using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateMappingTableForAddressDistrictAndLocalDeliveryProviderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressDistricts_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "AddressDistricts");

            migrationBuilder.DropIndex(
                name: "IX_AddressDistricts_LocalDeliveryProviderId",
                table: "AddressDistricts");

            migrationBuilder.DropColumn(
                name: "LocalDeliveryProviderId",
                table: "AddressDistricts");

            migrationBuilder.CreateTable(
                name: "LocalDeliveryProvider_AddressDistrict_Mappings",
                columns: table => new
                {
                    LocalDeliveryProviderId = table.Column<long>(type: "bigint", nullable: false),
                    AddressDistrictId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDeliveryProvider_AddressDistrict_Mappings", x => new { x.LocalDeliveryProviderId, x.AddressDistrictId });
                    table.ForeignKey(
                        name: "FK_LocalDeliveryProvider_AddressDistrict_Mappings_AddressDistricts_AddressDistrictId",
                        column: x => x.AddressDistrictId,
                        principalTable: "AddressDistricts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalDeliveryProvider_AddressDistrict_Mappings_LocalDeliveryProviders_LocalDeliveryProviderId",
                        column: x => x.LocalDeliveryProviderId,
                        principalTable: "LocalDeliveryProviders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalDeliveryProvider_AddressDistrict_Mappings_AddressDistrictId",
                table: "LocalDeliveryProvider_AddressDistrict_Mappings",
                column: "AddressDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDeliveryProvider_AddressDistrict_Mappings_LocalDeliveryProviderId_AddressDistrictId",
                table: "LocalDeliveryProvider_AddressDistrict_Mappings",
                columns: new[] { "LocalDeliveryProviderId", "AddressDistrictId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalDeliveryProvider_AddressDistrict_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "LocalDeliveryProviderId",
                table: "AddressDistricts",
                type: "bigint",
                nullable: true);

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
    }
}
