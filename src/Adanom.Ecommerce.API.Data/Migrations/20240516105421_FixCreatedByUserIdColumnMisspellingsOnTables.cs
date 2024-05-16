using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedByUserIdColumnMisspellingsOnTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "TaxAdministrations",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "ShippingProviders",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "PickUpStores",
                newName: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "TaxAdministrations",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ShippingProviders",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "PickUpStores",
                newName: "CreateByUserId");
        }
    }
}
