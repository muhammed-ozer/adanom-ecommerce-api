using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShippingFeeSubTotalNameToShippingFeeTotalOnOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingFeeSubTotal",
                table: "Orders",
                newName: "ShippingFeeTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingFeeTotal",
                table: "Orders",
                newName: "ShippingFeeSubTotal");
        }
    }
}
