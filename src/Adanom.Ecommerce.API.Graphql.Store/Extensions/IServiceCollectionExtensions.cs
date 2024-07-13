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
                .AddType<ShippingAddressQueries>();

            graphqlServices
                .AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<ShippingAddressMutations>();

            graphqlServices
                .AddType<ShippingAddressResolvers>();

            return services;
        }
    }
}
