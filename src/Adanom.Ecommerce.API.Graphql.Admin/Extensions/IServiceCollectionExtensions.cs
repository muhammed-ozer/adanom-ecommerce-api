using Adanom.Ecommerce.API.Graphql.Admin;
using Adanom.Ecommerce.API.Graphql.Admin.Mutations;
using Adanom.Ecommerce.API.Graphql.Admin.Queries;
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
                .AddType<TaxCategoryQueries>();

            #endregion

            #region Mutations

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<BrandMutations>()
                .AddType<TaxCategoryMutations>();

            #endregion

            return services;
        }
    }
}
