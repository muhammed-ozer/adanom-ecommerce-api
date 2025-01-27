using Adanom.Ecommerce.API.Caching;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();

            return services;
        }
    }
}
