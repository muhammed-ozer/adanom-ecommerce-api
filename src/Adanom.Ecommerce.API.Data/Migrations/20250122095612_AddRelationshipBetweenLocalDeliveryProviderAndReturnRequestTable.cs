using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenLocalDeliveryProviderAndReturnRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocalDeliveryProviderId",
                table: "ReturnRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequests_LocalDeliveryProviderId",
                table: "ReturnRequests",
                column: "LocalDeliveryProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnRequests_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "ReturnRequests",
                column: "LocalDeliveryProviderId",
                principalTable: "LocalDeliveryProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnRequests_LocalDeliveryProviders_LocalDeliveryProviderId",
                table: "ReturnRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReturnRequests_LocalDeliveryProviderId",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "LocalDeliveryProviderId",
                table: "ReturnRequests");
        }
    }
}
