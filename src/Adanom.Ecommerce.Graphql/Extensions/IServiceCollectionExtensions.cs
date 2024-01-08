using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationGraphql(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
