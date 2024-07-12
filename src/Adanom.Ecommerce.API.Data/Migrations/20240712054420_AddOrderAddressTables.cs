using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAddressTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_BillingAddresses_BillingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                table: "Orders",
                newName: "OrderShippingAddressId");

            migrationBuilder.RenameColumn(
                name: "BillingAddressId",
                table: "Orders",
                newName: "OrderBillingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                newName: "IX_Orders_OrderShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                newName: "IX_Orders_OrderBillingAddressId");

            migrationBuilder.CreateTable(
                name: "OrderBillingAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddressCityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressDistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxAdministration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBillingAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderShippingAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddressCityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressDistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShippingAddresses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders",
                column: "OrderBillingAddressId",
                principalTable: "OrderBillingAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderShippingAddresses_OrderShippingAddressId",
                table: "Orders",
                column: "OrderShippingAddressId",
                principalTable: "OrderShippingAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderShippingAddresses_OrderShippingAddressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderBillingAddresses");

            migrationBuilder.DropTable(
                name: "OrderShippingAddresses");

            migrationBuilder.RenameColumn(
                name: "OrderShippingAddressId",
                table: "Orders",
                newName: "ShippingAddressId");

            migrationBuilder.RenameColumn(
                name: "OrderBillingAddressId",
                table: "Orders",
                newName: "BillingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderShippingAddressId",
                table: "Orders",
                newName: "IX_Orders_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderBillingAddressId",
                table: "Orders",
                newName: "IX_Orders_BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_BillingAddresses_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId",
                principalTable: "BillingAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id");
        }
    }
}
