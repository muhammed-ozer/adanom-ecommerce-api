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
                .AddType<ProductQueries>()
                .AddType<ProductReviewQueries>()
                .AddType<ProductSKUQueries>()
                .AddType<ProductCategoryQueries>()
                .AddType<ProductSpecificationAttributeQueries>()
                .AddType<ProductSpecificationAttributeGroupQueries>()
                .AddType<ProductTagQueries>()
                .AddType<StockUnitTypeQueries>()
                .AddType<MetaInformationQueries>()
                .AddType<SliderItemQueries>()
                .AddType<ShippingProviderQueries>()
                .AddType<PickUpStoreQueries>()
                .AddType<TaxCategoryQueries>()
                .AddType<TaxAdministrationQueries>();

            #endregion

            #region Mutations

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<CompanyMutations>()
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
                .AddType<MetaInformationMutations>()
                .AddType<ImageMutations>()
                .AddType<SliderItemMutations>()
                .AddType<ShippingProviderMutations>()
                .AddType<PickUpStoreMutations>()
                .AddType<TaxCategoryMutations>()
                .AddType<TaxAdministrationMutations>();

            #endregion

            #region Resolvers

            graphqlServices
                .AddType<AddressDistrictResolvers>()
                .AddType<BrandResolvers>()
                .AddType<ProductResolvers>()
                .AddType<ProductCategoryResolvers>()
                .AddType<ProductSKUResolvers>()
                .AddType<ProductPriceResolvers>()
                .AddType<ProductSpecificationAttributeResolvers>()
                .AddType<ProductSpecificationAttributeGroupResolvers>()
                .AddType<SliderItemResolvers>();

            #endregion

            return services;
        }
    }
}
