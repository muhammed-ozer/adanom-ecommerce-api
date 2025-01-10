using Adanom.Ecommerce.API.Graphql.Admin;
using Adanom.Ecommerce.API.Graphql.Admin.Mutations;
using Adanom.Ecommerce.API.Graphql.Admin.Queries;
using Adanom.Ecommerce.API.Graphql.Admin.Resolvers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphqlAdmin(this IServiceCollection services, IConfiguration configuration)
        {
            var graphqlServices = services.AddGraphQLServer(schemaName: "admin", maxAllowedRequestSize: int.MaxValue)
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddType<UploadType>();

            #region Queries

            graphqlServices
                .AddQueryType(e => e.Name(OperationTypeNames.Query))
                .AddType<CompanyQueries>()
                .AddType<AddressCityQueries>()
                .AddType<AddressDistrictQueries>()
                .AddType<BrandQueries>()
                .AddType<FavoriteItemQueries>()
                .AddType<ProductQueries>()
                .AddType<ProductReviewQueries>()
                .AddType<ProductSKUQueries>()
                .AddType<ProductCategoryQueries>()
                .AddType<ProductSpecificationAttributeQueries>()
                .AddType<ProductSpecificationAttributeGroupQueries>()
                .AddType<ProductTagQueries>()
                .AddType<StockUnitTypeQueries>()
                .AddType<SliderItemQueries>()
                .AddType<SliderItemTypeQueries>()
                .AddType<ShippingProviderQueries>()
                .AddType<LocalDeliveryProviderQueries>()
                .AddType<StockNotificationItemQueries>()
                .AddType<PickUpStoreQueries>()
                .AddType<TaxCategoryQueries>()
                .AddType<AnonymousShoppingCartQueries>()
                .AddType<AnonymousShoppingCartItemQueries>()
                .AddType<ShoppingCartQueries>()
                .AddType<ShoppingCartItemQueries>()
                .AddType<OrderQueries>()
                .AddType<OrderItemQueries>()
                .AddType<OrderPaymentQueries>()
                .AddType<OrderStatusTypeQueries>()
                .AddType<DeliveryTypeQueries>()
                .AddType<ReturnRequestQueries>()
                .AddType<ReturnRequestItemQueries>()
                .AddType<ReturnRequestStatusTypeQueries>()
                .AddType<UserQueries>()
                .AddType<RoleQueries>()
                .AddType<NotificationQueries>()
                .AddType<NotificationTypeQueries>();

            #endregion

            #region Mutations

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<CompanyMutations>()
                .AddType<NotificationMutations>()
                .AddType<AddressCityMutations>()
                .AddType<AddressDistrictMutations>()
                .AddType<BrandMutations>()
                .AddType<ProductCategoryMutations>()
                .AddType<Product_ProductCategoryMutations>()
                .AddType<Product_ProductSpecificationAttributeMutations>()
                .AddType<Product_ProductTagMutations>()
                .AddType<ProductSpecificationAttributeMutations>()
                .AddType<ProductSpecificationAttributeGroupMutations>()
                .AddType<ProductTagMutations>()
                .AddType<ProductMutations>()
                .AddType<ProductReviewMutations>()
                .AddType<ProductSKUMutations>()
                .AddType<ProductPriceMutations>()
                .AddType<ImageMutations>()
                .AddType<SliderItemMutations>()
                .AddType<ShippingProviderMutations>()
                .AddType<LocalDeliveryProviderMutations>()
                .AddType<PickUpStoreMutations>()
                .AddType<TaxCategoryMutations>()
                .AddType<UserMutations>()
                .AddType<OrderMutations>()
                .AddType<ReturnRequestMutations>();

            #endregion

            #region Resolvers

            graphqlServices
                .AddType<AddressDistrictResolvers>()
                .AddType<ShippingAddressResolvers>()
                .AddType<BillingAddressResolvers>()
                .AddType<BrandResolvers>()
                .AddType<FavoriteItemResolvers>()
                .AddType<ProductResolvers>()
                .AddType<ProductReviewResolvers>()
                .AddType<ProductCategoryResolvers>()
                .AddType<ProductSKUResolvers>()
                .AddType<ProductPriceResolvers>()
                .AddType<ProductSpecificationAttributeResolvers>()
                .AddType<ProductSpecificationAttributeGroupResolvers>()
                .AddType<SliderItemResolvers>()
                .AddType<StockNotificationItemResolvers>()
                .AddType<LocalDeliveryProviderResolvers>()
                .AddType<AnonymousShoppingCartResolvers>()
                .AddType<AnonymousShoppingCartItemResolvers>()
                .AddType<ShoppingCartResolvers>()
                .AddType<ShoppingCartItemResolvers>()
                .AddType<OrderResolvers>()
                .AddType<OrderItemResolvers>()
                .AddType<ReturnRequestResolvers>()
                .AddType<ReturnRequestItemResolvers>()
                .AddType<NotificationResolvers>();

            #endregion

            return services;
        }
    }
}
