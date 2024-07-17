using Microsoft.Extensions.Configuration;
using Adanom.Ecommerce.API.Graphql.Store;
using Adanom.Ecommerce.API.Graphql.Store.Queries;
using Adanom.Ecommerce.API.Graphql.Store.Mutations;
using Adanom.Ecommerce.API.Graphql.Store.Resolvers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphqlStore(this IServiceCollection services, IConfiguration configuration)
        {
            var graphqlServices = services.AddGraphQLServer(schemaName: "store", maxAllowedRequestSize: int.MaxValue)
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddType<UploadType>();

            graphqlServices
                .AddQueryType(e => e.Name(OperationTypeNames.Query))
                .AddType<AddressCityQueries>()
                .AddType<AddressDistrictQueries>()
                .AddType<ProductQueries>()
                .AddType<ProductReviewQueries>()
                .AddType<ProductSKUQueries>()
                .AddType<ProductSpecificationAttributeGroupQueries>()
                .AddType<ProductSpecificationAttributeQueries>()
                .AddType<ProductTagQueries>()
                .AddType<TaxCategoryQueries>()
                .AddType<BrandQueries>()
                .AddType<ProductCategoryQueries>()
                .AddType<FavoriteItemQueries>()
                .AddType<StockNotificationItemQueries>()
                .AddType<CompanyQueries>()
                .AddType<PickUpStoreQueries>()
                .AddType<ShippingProviderQueries>()
                .AddType<ShippingAddressQueries>()
                .AddType<BillingAddressQueries>()
                .AddType<SliderItemQueries>()
                .AddType<ShoppingCartQueries>()
                .AddType<ShoppingCartItemQueries>()
                .AddType<AnonymousShoppingCartItemQueries>();

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<ProductReveiwMutations>()
                .AddType<FavoriteItemMutations>()
                .AddType<StockNotificationItemMutations>()
                .AddType<ShippingAddressMutations>()
                .AddType<BillingAddressMutations>()
                .AddType<ShoppingCartItemMutations>()
                .AddType<ShoppingCartMutations>()
                .AddType<AnonymousShoppingCartItemMutations>();

            graphqlServices
                .AddType<BrandResolvers>()
                .AddType<ProductCategoryResolvers>()
                .AddType<ProductPriceResolvers>()
                .AddType<ProductResolvers>()
                .AddType<ProductReviewResolvers>()
                .AddType<ProductSpecificationAttributeGroupResolvers>()
                .AddType<ProductSpecificationAttributeResolvers>()
                .AddType<FavoriteItemResolvers>()
                .AddType<StockNotificationItemResolvers>()
                .AddType<ShippingAddressResolvers>()
                .AddType<BillingAddressResolvers>()
                .AddType<SliderItemResolvers>()
                .AddType<ShoppingCartResolvers>();

            return services;
        }
    }
}
