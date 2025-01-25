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

            modelBuilder.Entity<AddressCity>()
                .Property(e => e.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<AddressDistrict>()
                .Property(e => e.Id)
                .ValueGeneratedNever();

            #region Product

            modelBuilder.Entity<Product>()
                    .HasMany(e => e.ProductReviews)
                    .WithOne(e => e.Product)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ProductSKUMapping

            modelBuilder.Entity<Product_ProductSKU_Mapping>()
                    .HasKey(e => new { e.ProductId, e.ProductSKUId });

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

            #endregion

            #region ProductSpecificationAttributeMapping

            modelBuilder.Entity<Product_ProductSpecificationAttribute_Mapping>()
                    .HasKey(e => new { e.ProductId, e.ProductSpecificationAttributeId });

            #endregion

            #region ProductCategoryMapping

            modelBuilder.Entity<Product_ProductCategory_Mapping>()
                    .HasKey(e => new { e.ProductCategoryId, e.ProductId });

            #endregion

            #region LocalDeliveryProvider_AddressDistrict_Mapping

            modelBuilder.Entity<LocalDeliveryProvider_AddressDistrict_Mapping>()
                    .HasKey(e => new { e.LocalDeliveryProviderId, e.AddressDistrictId });

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

            #region Product - ProductSKU

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_ProductSKU_Mappings)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductSKU>()
                .HasMany(e => e.Product_ProductSKU_Mappings)
                .WithOne(e => e.ProductSKU);

            #endregion

            #region Product - ProductSpecificationAttribute

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_ProductSpecificationAttribute_Mappings)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductSpecificationAttribute>()
                .HasMany(e => e.Product_ProductSpecificationAttribute_Mappings)
                .WithOne(e => e.ProductSpecificationAttribute);

            #endregion

            #region Product - ProductCategory

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_ProductCategory_Mappings)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Product_ProductCategory_Mappings)
                .WithOne(e => e.ProductCategory);

            #endregion

            #region LocalDeliveryProvider - AddressDistrict

            modelBuilder.Entity<LocalDeliveryProvider>()
                .HasMany(e => e.LocalDeliveryProvider_AddressDistrict_Mappings)
                .WithOne(e => e.LocalDeliveryProvider);

            modelBuilder.Entity<AddressDistrict>()
                .HasMany(e => e.LocalDeliveryProvider_AddressDistrict_Mappings)
                .WithOne(e => e.AddressDistrict);

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
                e.Property(e => e.ShippingFeeTotal).HasPrecision(10, 2);
                e.Property(e => e.ShippingFeeTax).HasPrecision(10, 2);
                e.Property(e => e.SubTotal).HasPrecision(10, 2);
                e.Property(e => e.TotalDiscount).HasPrecision(10, 2);
                e.Property(e => e.UserDefaultDiscountRateBasedDiscount).HasPrecision(10, 2);
                e.Property(e => e.DiscountByOrderPaymentType).HasPrecision(10, 2);
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

            modelBuilder.Entity<ShippingProvider>(e =>
            {
                e.Property(e => e.FeeTotal).HasPrecision(10, 2);
                e.Property(e => e.MinimumOrderGrandTotal).HasPrecision(10, 2);
                e.Property(e => e.MinimumFreeShippingOrderGrandTotal).HasPrecision(10, 2);
            });

            modelBuilder.Entity<LocalDeliveryProvider>(e =>
            {
                e.Property(e => e.FeeTotal).HasPrecision(10, 2);
                e.Property(e => e.MinimumFreeDeliveryOrderGrandTotal).HasPrecision(10, 2);
                e.Property(e => e.MinimumOrderGrandTotal).HasPrecision(10, 2);
            });

            modelBuilder.Entity<AnonymousShoppingCartItem>(e =>
            {
                e.Property(e => e.OriginalPrice).HasPrecision(10, 2);
                e.Property(e => e.DiscountedPrice).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ShoppingCartItem>(e =>
            {
                e.Property(e => e.OriginalPrice).HasPrecision(10, 2);
                e.Property(e => e.DiscountedPrice).HasPrecision(10, 2);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.Property(e => e.DefaultDiscountRate).HasPrecision(10, 2);
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

        public DbSet<FavoriteItem> FavoriteItems { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

        public DbSet<Product_ProductCategory_Mapping> Product_ProductCategory_Mappings { get; set; } = null!;

        public DbSet<ProductReview> ProductReviews { get; set; } = null!;

        public DbSet<ProductSpecificationAttribute> ProductSpecificationAttributes { get; set; } = null!;

        public DbSet<ProductSpecificationAttributeGroup> ProductSpecificationAttributeGroups { get; set; } = null!;

        public DbSet<Product_ProductSpecificationAttribute_Mapping> Product_ProductSpecificationAttribute_Mappings { get; set; } = null!;

        public DbSet<ProductSKU> ProductSKUs { get; set; } = null!;

        public DbSet<Product_ProductSKU_Mapping> Product_ProductSKU_Mappings { get; set; } = null!;

        public DbSet<ProductTag> ProductTags { get; set; } = null!;

        public DbSet<ProductPrice> ProductPrices { get; set; } = null!;

        public DbSet<TaxCategory> TaxCategories { get; set; } = null!;

        public DbSet<Product_ProductTag_Mapping> Product_ProductTag_Mappings { get; set; } = null!;

        public DbSet<StockNotificationItem> StockNotificationItems { get; set; } = null!;

        public DbSet<StockReservation> StockReservations { get; set; } = null!;

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

        public DbSet<LocalDeliveryProvider> LocalDeliveryProviders { get; set; } = null!;

        public DbSet<PickUpStore> PickUpStores { get; set; } = null!;

        public DbSet<LocalDeliveryProvider_AddressDistrict_Mapping> LocalDeliveryProvider_AddressDistrict_Mappings { get; set; } = null!;

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

        public DbSet<OrderBillingAddress> OrderBillingAddresses { get; set; } = null!;

        public DbSet<OrderShippingAddress> OrderShippingAddresses { get; set; } = null!;

        #endregion
    }
}
