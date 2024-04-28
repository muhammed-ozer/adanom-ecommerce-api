using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageAndMetaInformationMappingsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brand_Image_Mappings");

            migrationBuilder.DropTable(
                name: "Brand_MetaInformation_Mappings");

            migrationBuilder.DropTable(
                name: "Product_Image_Mappings");

            migrationBuilder.DropTable(
                name: "Product_MetaInformation_Mappings");

            migrationBuilder.DropTable(
                name: "ProductCategory_Image_Mappings");

            migrationBuilder.DropTable(
                name: "ProductCategory_MetaInformation_Mappings");

            migrationBuilder.CreateTable(
                name: "Image_Entity_Mappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false),
                    ImageType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image_Entity_Mappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaInformation_Entity_Mappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaInformationId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaInformation_Entity_Mappings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_Entity_Mappings_ImageId",
                table: "Image_Entity_Mappings",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaInformation_Entity_Mappings_MetaInformationId",
                table: "MetaInformation_Entity_Mappings",
                column: "MetaInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image_Entity_Mappings");

            migrationBuilder.DropTable(
                name: "MetaInformation_Entity_Mappings");

            migrationBuilder.CreateTable(
                name: "Brand_Image_Mappings",
                columns: table => new
                {
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ImageType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand_Image_Mappings", x => new { x.BrandId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_Brand_Image_Mappings_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brand_Image_Mappings_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand_MetaInformation_Mappings",
                columns: table => new
                {
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    MetaInformationId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand_MetaInformation_Mappings", x => new { x.BrandId, x.MetaInformationId });
                    table.ForeignKey(
                        name: "FK_Brand_MetaInformation_Mappings_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brand_MetaInformation_Mappings_MetaInformations_MetaInformationId",
                        column: x => x.MetaInformationId,
                        principalTable: "MetaInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Image_Mappings",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ImageType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Image_Mappings", x => new { x.ProductId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_Product_Image_Mappings_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Image_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_MetaInformation_Mappings",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    MetaInformationId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_MetaInformation_Mappings", x => new { x.ProductId, x.MetaInformationId });
                    table.ForeignKey(
                        name: "FK_Product_MetaInformation_Mappings_MetaInformations_MetaInformationId",
                        column: x => x.MetaInformationId,
                        principalTable: "MetaInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_MetaInformation_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory_Image_Mappings",
                columns: table => new
                {
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ImageType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory_Image_Mappings", x => new { x.ProductCategoryId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Image_Mappings_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Image_Mappings_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory_MetaInformation_Mappings",
                columns: table => new
                {
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    MetaInformationId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory_MetaInformation_Mappings", x => new { x.ProductCategoryId, x.MetaInformationId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_MetaInformation_Mappings_MetaInformations_MetaInformationId",
                        column: x => x.MetaInformationId,
                        principalTable: "MetaInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_MetaInformation_Mappings_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Image_Mappings_BrandId_ImageId",
                table: "Brand_Image_Mappings",
                columns: new[] { "BrandId", "ImageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Image_Mappings_ImageId",
                table: "Brand_Image_Mappings",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_MetaInformation_Mappings_MetaInformationId_BrandId",
                table: "Brand_MetaInformation_Mappings",
                columns: new[] { "MetaInformationId", "BrandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Image_Mappings_ImageId",
                table: "Product_Image_Mappings",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Image_Mappings_ProductId_ImageId",
                table: "Product_Image_Mappings",
                columns: new[] { "ProductId", "ImageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_MetaInformation_Mappings_MetaInformationId_ProductId",
                table: "Product_MetaInformation_Mappings",
                columns: new[] { "MetaInformationId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_Image_Mappings_ImageId",
                table: "ProductCategory_Image_Mappings",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_Image_Mappings_ProductCategoryId_ImageId",
                table: "ProductCategory_Image_Mappings",
                columns: new[] { "ProductCategoryId", "ImageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_MetaInformation_Mappings_MetaInformationId_ProductCategoryId",
                table: "ProductCategory_MetaInformation_Mappings",
                columns: new[] { "MetaInformationId", "ProductCategoryId" },
                unique: true);
        }
    }
}
