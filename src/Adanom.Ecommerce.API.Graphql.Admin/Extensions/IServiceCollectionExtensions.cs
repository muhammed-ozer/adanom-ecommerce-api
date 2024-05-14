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
                .AddType<BrandQueries>()
                .AddType<ProductQueries>()
                .AddType<ProductSKUQueries>()
                .AddType<ProductCategoryQueries>()
                .AddType<ProductSpecificationAttributeQueries>()
                .AddType<ProductSpecificationAttributeGroupQueries>()
                .AddType<ProductTagQueries>()
                .AddType<StockUnitTypeQueries>()
                .AddType<MetaInformationQueries>()
                .AddType<TaxCategoryQueries>();

            #endregion

            #region Mutations
            
            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<BrandMutations>()
                .AddType<ProductCategoryMutations>()
                .AddType<Product_ProductCategoryMutations>()
                .AddType<Product_ProductSpecificationAttributeMutations>()
                .AddType<Product_ProductTagMutations>()
                .AddType<ProductSpecificationAttributeMutations>()
                .AddType<ProductSpecificationAttributeGroupMutations>()
                .AddType<ProductTagMutations>()
                .AddType<ProductMutations>()
                .AddType<ProductSKUMutations>()
                .AddType<ProductPriceMutations>()
                .AddType<TaxCategoryMutations>();

            #endregion

            #region Resolvers

            graphqlServices
                .AddType<BrandResolvers>()
                .AddType<ProductResolvers>()
                .AddType<ProductCategoryResolvers>()
                .AddType<ProductSKUResolvers>()
                .AddType<ProductPriceResolvers>()
                .AddType<ProductSpecificationAttributeResolvers>()
                .AddType<ProductSpecificationAttributeGroupResolvers>();

            #endregion

            return services;
        }
    }
}
