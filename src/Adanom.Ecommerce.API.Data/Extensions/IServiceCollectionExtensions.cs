using Adanom.Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationData(this IServiceCollection services, IConfiguration configuration)
        {
            Action<DbContextOptionsBuilder> configureApplicationDbContext = options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
            };

            services.AddDbContext<ApplicationDbContext>(
                configureApplicationDbContext,
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Singleton);

            services.AddDbContextFactory<ApplicationDbContext>(configureApplicationDbContext);

            return services;
        }
    }
}
