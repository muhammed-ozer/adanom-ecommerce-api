﻿using Adanom.Ecommerce.API.Graphql.DataLoaders;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphqlDataLoader(this IServiceCollection services, IConfiguration configuration)
        {
            var graphqlServices = services.AddGraphQL();

            graphqlServices
                .AddDataLoader<ProductByIdDataLoader>()
                .AddDataLoader<ProductSKUByProductIdDataLoader>()
                .AddDataLoader<ProductSKUByCodeDataLoader>()
                .AddDataLoader<ProductPriceByIdDataLoader>()
                .AddDataLoader<ImagesByEntityDataLoader>()
                .AddDataLoader<EntityImageDataLoader>()
                .AddDataLoader<DefaultEntityImageDataLoader>();

            return services;
        }
    }
}
