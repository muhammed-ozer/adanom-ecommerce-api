using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageTypeColumnAndAddIsDefaultColumnToImage_Entity_MappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Image_Entity_Mappings",
                table: "Image_Entity_Mappings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Image_Entity_Mappings");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Image_Entity_Mappings");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Image_Entity_Mappings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image_Entity_Mappings",
                table: "Image_Entity_Mappings",
                columns: new[] { "ImageId", "EntityId", "EntityType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Image_Entity_Mappings",
                table: "Image_Entity_Mappings");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Image_Entity_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Image_Entity_Mappings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte>(
                name: "ImageType",
                table: "Image_Entity_Mappings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image_Entity_Mappings",
                table: "Image_Entity_Mappings",
                column: "Id");
        }
    }
}
