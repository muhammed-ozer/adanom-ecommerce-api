using Adanom.Ecommerce.API.Services;
using Adanom.Ecommerce.API.Services.Implementations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationMailServices(configuration);
            services.AddApplicationAzureServices(configuration);

            services.AddScoped<ICalculationService, CalculationService>();

            return services;
        }
    }
}
