using Adanom.Ecommerce.API.Graphql.Auth;
using Adanom.Ecommerce.API.Graphql.Auth.Mutations;
using Adanom.Ecommerce.API.Graphql.Auth.Queries;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphqlAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var graphqlServices = services.AddGraphQLServer(schemaName: "auth", maxAllowedRequestSize: int.MaxValue)
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddType<UploadType>();

            graphqlServices
                .AddQueryType(e => e.Name(OperationTypeNames.Query))
                .AddType<TestQueries>();

            graphqlServices.AddMutationType(e => e.Name(OperationTypeNames.Mutation))
                .AddType<AuthMutations>();

            return services;
        }
    }
}
