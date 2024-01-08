using System.Reflection;
using Adanom.Ecommerce.Commands.Models;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCommands(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(e =>
            {
                e.AddMaps(Assembly.GetAssembly(typeof(TestModel)));
            });

            return services;
        }
    }
}
