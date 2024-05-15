using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImage_Entity_MappingTableAndMergeWithImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image_Entity_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Images",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "EntityType",
                table: "Images",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "Image_Entity_Mappings",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image_Entity_Mappings", x => new { x.ImageId, x.EntityId, x.EntityType });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_Entity_Mappings_ImageId",
                table: "Image_Entity_Mappings",
                column: "ImageId",
                unique: true);
        }
    }
}
