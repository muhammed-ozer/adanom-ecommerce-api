using Adanom.Ecommerce.API.Graphql.Frontend;
using Adanom.Ecommerce.API.Graphql.Frontend.Queries;
using HotChocolate.Types;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphqlFrontend(this IServiceCollection services, IConfiguration configuration)
        {
            var graphqlServices = services.AddGraphQLServer(schemaName: "frontend", maxAllowedRequestSize: int.MaxValue)
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddType<UploadType>();

            graphqlServices
                .AddQueryType(e => e.Name(OperationTypeNames.Query))
                .AddType<TestQueries>();

            return services;
        }
    }
}
