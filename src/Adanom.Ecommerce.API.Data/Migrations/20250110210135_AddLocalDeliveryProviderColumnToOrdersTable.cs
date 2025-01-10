using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalDeliveryProviderColumnToOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocalDeliveryProviderId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocalDeliveryProviderId",
                table: "Orders",
                column: "LocalDeliveryProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "Orders",
                column: "LocalDeliveryProviderId",
                principalTable: "LocalDeliveryProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LocalDeliveryProviderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LocalDeliveryProviderId",
                table: "Orders");
        }
    }
}
