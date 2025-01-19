using Adanom.Ecommerce.API.Services;
using Adanom.Ecommerce.API.Services.Implementations;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationMailServices(configuration);
            services.AddApplicationAzureServices(configuration);

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddScoped<ICalculationService, CalculationService>();
            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();

            return services;
        }
    }
}
