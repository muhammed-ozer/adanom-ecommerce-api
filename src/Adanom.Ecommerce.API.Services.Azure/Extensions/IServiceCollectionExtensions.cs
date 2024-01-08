using Adanom.Ecommerce.API.Services.Azure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationAzureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(configuration.GetSection("Azure:ConnectionString").Value);
            });

            services.AddScoped<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
