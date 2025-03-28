﻿using Microsoft.Extensions.Configuration;
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
                .AddType<LocalDeliveryProviderQueries>()
                .AddType<ShippingAddressQueries>()
                .AddType<BillingAddressQueries>()
                .AddType<SliderItemQueries>()
                .AddType<ShoppingCartQueries>()
                .AddType<ShoppingCartItemQueries>()
                .AddType<AnonymousShoppingCartQueries>()
                .AddType<AnonymousShoppingCartItemQueries>()
                .AddType<OrderQueries>()
                .AddType<OrderDocumentQueries>()
                .AddType<OrderPaymentTypeQueries>()
                .AddType<ReturnRequestQueries>()
                .AddType<CheckoutQueries>();

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<ProductReveiwMutations>()
                .AddType<FavoriteItemMutations>()
                .AddType<StockNotificationItemMutations>()
                .AddType<ShippingAddressMutations>()
                .AddType<BillingAddressMutations>()
                .AddType<ShoppingCartItemMutations>()
                .AddType<ShoppingCartMutations>()
                .AddType<AnonymousShoppingCartItemMutations>()
                .AddType<CheckoutMutations>()
                .AddType<OrderMutations>()
                .AddType<ReturnRequestMutations>()
                .AddType<UserMutations>();

            graphqlServices
                .AddType<BrandResolvers>()
                .AddType<ProductCategoryResolvers>()
                .AddType<ProductPriceResolvers>()
                .AddType<ProductSKUResolvers>()
                .AddType<ProductResolvers>()
                .AddType<ProductReviewResolvers>()
                .AddType<ProductSpecificationAttributeGroupResolvers>()
                .AddType<ProductSpecificationAttributeResolvers>()
                .AddType<FavoriteItemResolvers>()
                .AddType<StockNotificationItemResolvers>()
                .AddType<ShippingAddressResolvers>()
                .AddType<BillingAddressResolvers>()
                .AddType<SliderItemResolvers>()
                .AddType<LocalDeliveryProviderResolvers>()
                .AddType<ShoppingCartItemResolvers>()
                .AddType<AnonymousShoppingCartResolvers>()
                .AddType<AnonymousShoppingCartItemResolvers>()
                .AddType<OrderResolvers>()
                .AddType<OrderPaymentResolvers>()
                .AddType<OrderItemResolvers>()
                .AddType<ReturnRequestResolvers>()
                .AddType<ReturnRequestItemResolvers>();

            return services;
        }
    }
}
