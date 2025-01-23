using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisapprovedMessageColumnToReturnRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReasonMessage",
                table: "ReturnRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisapprovedReasonMessage",
                table: "ReturnRequests");
        }
    }
}
