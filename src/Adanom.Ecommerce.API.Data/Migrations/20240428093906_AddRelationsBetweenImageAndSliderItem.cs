using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsBetweenImageAndSliderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "SliderItems");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "SliderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SliderItems_ImageId",
                table: "SliderItems",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderItems_Images_ImageId",
                table: "SliderItems",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderItems_Images_ImageId",
                table: "SliderItems");

            migrationBuilder.DropIndex(
                name: "IX_SliderItems_ImageId",
                table: "SliderItems");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "SliderItems");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "SliderItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
