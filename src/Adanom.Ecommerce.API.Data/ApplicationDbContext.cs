using Adanom.Ecommerce.API.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            #region Product

            modelBuilder.Entity<Product>()
                    .HasMany(e => e.ProductReviews)
                    .WithOne(e => e.Product)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductImageMapping

            modelBuilder.Entity<Product_Image_Mapping>()
                    .HasKey(e => new { e.ProductId, e.ImageId });

            #endregion

            #region ProductAttribute

            modelBuilder.Entity<ProductAttribute>()
                    .HasMany(e => e.ProductAttributeOptions)
                    .WithOne(e => e.ProductAttribute)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductTagMapping

            modelBuilder.Entity<Product_ProductTag_Mapping>()
                    .HasKey(e => new { e.ProductId, e.ProductTagId });

            #endregion

            #region ProductSpecificationAttribute

            modelBuilder.Entity<ProductSpecificationAttributeGroup>()
                    .HasMany(e => e.SpecificationAttributes)
                    .WithOne(e => e.SpecificationAttributeGroup)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSpecificationAttribute>()
                .HasMany(e => e.SpecificationAttributeOptions)
                .WithOne(e => e.SpecificationAttribute)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductSpecificationAttributeOptionMapping

            modelBuilder.Entity<Product_ProductSpecificationAttributeOption_Mapping>()
                    .HasKey(e => new { e.ProductId, e.ProductSpecificationAttributeOptionId });

            #endregion

            #region ProductMetaInformationMapping

            modelBuilder.Entity<Product_MetaInformation_Mapping>()
                    .HasKey(e => new { e.ProductId, e.MetaInformationId });

            #endregion

            #region ProductCategoryMapping

            modelBuilder.Entity<Product_ProductCategory_Mapping>()
                    .HasKey(e => new { e.ProductCategoryId, e.ProductId });

            #endregion

            #region ProductCategoryMetaInformationMapping

            modelBuilder.Entity<ProductCategory_MetaInformation_Mapping>()
                    .HasKey(e => new { e.ProductCategoryId, e.MetaInformationId });

            #endregion

            #region ProductCategoryImageMapping

            modelBuilder.Entity<ProductCategory_Image_Mapping>()
                    .HasKey(e => new { e.ProductCategoryId, e.ImageId });

            #endregion

            #region BrandMetaInformationMapping

            modelBuilder.Entity<Brand_MetaInformation_Mapping>()
                    .HasKey(e => new { e.BrandId, e.MetaInformationId });

            #endregion

            #region BrandImageMapping

            modelBuilder.Entity<Brand_Image_Mapping>()
                    .HasKey(e => new { e.BrandId, e.ImageId });

            #endregion

            #region Order

            modelBuilder.Entity<Order>()
                   .HasMany(e => e.Items)
                   .WithOne(e => e.Order)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ReturnRequest

            modelBuilder.Entity<ReturnRequest>()
                   .HasMany(e => e.Items)
                   .WithOne(e => e.ReturnRequest)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Relations

            #region Brand - Image

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Brand_Image_Mappings)
                .WithOne(e => e.Brand)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Brand_Image_Mappings)
                .WithOne(e => e.Image)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Brand - MetaInformation

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Brand_MetaInformation_Mappings)
                .WithOne(e => e.Brand)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MetaInformation>()
                .HasMany(e => e.Brand_MetaInformation_Mappings)
                .WithOne(e => e.MetaInformation)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Product - Image

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Image_Mappings)
                .WithOne(e => e.Product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Product_Image_Mappings)
                .WithOne(e => e.Image)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Product - ProductSpecificationAttributeOption

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_ProductSpecificationAttributeOption_Mappings)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductSpecificationAttributeOption>()
                .HasMany(e => e.Product_ProductSpecificationAttributeOption_Mappings)
                .WithOne(e => e.ProductSpecificationAttributeOption);

            #endregion

            #region Product - MetaInformation

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_MetaInformation_Mappings)
                .WithOne(e => e.Product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MetaInformation>()
                .HasMany(e => e.Product_MetaInformation_Mappings)
                .WithOne(e => e.MetaInformation)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductCategory - Image

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory_Image_Mappings)
                .WithOne(e => e.ProductCategory)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.ProductCategory_Image_Mappings)
                .WithOne(e => e.Image)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductCategory - MetaInformation

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory_MetaInformation_Mappings)
                .WithOne(e => e.ProductCategory)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MetaInformation>()
                .HasMany(e => e.ProductCategory_MetaInformation_Mappings)
                .WithOne(e => e.MetaInformation)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Product - ProductCategory

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_ProductCategory_Mappings)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Product_ProductCategory_Mappings)
                .WithOne(e => e.ProductCategory);

            #endregion

            #region AnonymousShoppingCart

            modelBuilder.Entity<AnonymousShoppingCart>()
                    .HasMany(e => e.Items)
                    .WithOne(e => e.AnonymousShoppingCart)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ShoppingCart

            modelBuilder.Entity<ShoppingCart>()
                    .HasMany(e => e.Items)
                    .WithOne(e => e.ShoppingCart)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #endregion

            #region Precisions

            modelBuilder.Entity<Order>(e =>
            {
                e.Property(e => e.GrandTotal).HasPrecision(10, 2);
                e.Property(e => e.ShippingFeeSubTotal).HasPrecision(10, 2);
                e.Property(e => e.ShippingFeeTax).HasPrecision(10, 2);
                e.Property(e => e.SubTotal).HasPrecision(10, 2);
                e.Property(e => e.SubTotalDiscount).HasPrecision(10, 2);
                e.Property(e => e.TaxTotal).HasPrecision(10, 2);
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.Property(e => e.DiscountTotal).HasPrecision(10, 2);
                e.Property(e => e.Price).HasPrecision(10, 2);
                e.Property(e => e.SubTotal).HasPrecision(10, 2);
                e.Property(e => e.TaxTotal).HasPrecision(10, 2);
                e.Property(e => e.Total).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ProductPrice>(e =>
            {
                e.Property(e => e.DiscountedPrice).HasPrecision(10, 2);
                e.Property(e => e.OriginalPrice).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ReturnRequest>(e =>
            {
                e.Property(e => e.GrandTotal).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ReturnRequestItem>(e =>
            {
                e.Property(e => e.Price).HasPrecision(10, 2);
                e.Property(e => e.Total).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ShippingSettings>(e =>
            {
                e.Property(e => e.FeeTax).HasPrecision(10, 2);
                e.Property(e => e.FeeTotal).HasPrecision(10, 2);
                e.Property(e => e.FeeWithoutTax).HasPrecision(10, 2);
                e.Property(e => e.MinimumFreeShippingTotalPrice).HasPrecision(10, 2);
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region Address DbSets

        public DbSet<AddressCity> AddressCities { get; set; } = null!;

        public DbSet<AddressDistrict> AddressDistricts { get; set; } = null!;

        public DbSet<BillingAddress> BillingAddresses { get; set; } = null!;

        public DbSet<ShippingAddress> ShippingAddresses { get; set; } = null!;

        #endregion

        #region Catalog DbSets

        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Brand_Image_Mapping> Brand_Image_Mappings { get; set; } = null!;

        public DbSet<Brand_MetaInformation_Mapping> Brand_MetaInformation_Mappings { get; set; } = null!;

        public DbSet<FavoriteItem> FavoriteItems { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

        public DbSet<ProductCategory_Image_Mapping> ProductCategory_Image_Mappings { get; set; } = null!;

        public DbSet<ProductCategory_MetaInformation_Mapping> ProductCategory_MetaInformation_Mappings { get; set; } = null!;

        public DbSet<Product_ProductCategory_Mapping> Product_ProductCategory_Mapping { get; set; } = null!;

        public DbSet<Product_Image_Mapping> Product_Image_Mappings { get; set; } = null!;

        public DbSet<Product_MetaInformation_Mapping> Product_MetaInformation_Mappings { get; set; } = null!;

        public DbSet<ProductReview> ProductReviews { get; set; } = null!;

        public DbSet<ProductSpecificationAttribute> ProductSpecificationAttributes { get; set; } = null!;

        public DbSet<ProductSpecificationAttributeGroup> ProductSpecificationAttributeGroups { get; set; } = null!;

        public DbSet<Product_ProductSpecificationAttributeOption_Mapping> Product_ProductSpecificationAttributeOption_Mappings { get; set; } = null!;

        public DbSet<ProductSpecificationAttributeOption> ProductSpecificationAttributeOptions { get; set; } = null!;

        public DbSet<ProductSKU> ProductSKUs { get; set; } = null!;

        public DbSet<ProductTag> ProductTags { get; set; } = null!;

        public DbSet<ProductPrice> ProductPrices { get; set; } = null!;

        public DbSet<ProductTax> ProductTaxes { get; set; } = null!;

        public DbSet<Product_ProductTag_Mapping> Product_ProductTag_Mappings { get; set; } = null!;

        public DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;

        public DbSet<ProductAttributeOption> ProductAttributeOptions { get; set; } = null!;

        public DbSet<StockNotificationItem> StockNotificationItems { get; set; } = null!;

        #endregion

        #region Cms DbSets

        public DbSet<SliderItem> SliderItems { get; set; } = null!;

        #endregion

        #region Common DbSets

        public DbSet<Company> Companies { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<MailTemplate> MailTemplates { get; set; } = null!;

        public DbSet<Notification> Notifications { get; set; } = null!;

        public DbSet<ShippingProvider> ShippingProviders { get; set; } = null!;

        public DbSet<ShippingSettings> ShippingSettings { get; set; } = null!;

        public DbSet<TaxAdministration> TaxAdministrations { get; set; } = null!;

        #endregion

        #region Order DbSets

        public DbSet<AnonymousShoppingCart> AnonymousShoppingCarts { get; set; } = null!;

        public DbSet<AnonymousShoppingCartItem> AnonymousShoppingCartItems { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public DbSet<OrderPayment> OrderPayments { get; set; } = null!;

        public DbSet<ReturnRequest> ReturnRequests { get; set; } = null!;

        public DbSet<ReturnRequestItem> ReturnRequestItems { get; set; } = null!;

        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;

        #endregion

        #region Seo DbSets

        public DbSet<MetaInformation> MetaInformations { get; set; } = null!;

        #endregion
    }
}
