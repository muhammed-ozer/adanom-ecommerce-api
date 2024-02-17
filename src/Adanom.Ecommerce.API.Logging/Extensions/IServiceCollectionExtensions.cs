using Adanom.Ecommerce.API.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LogConnection"));
            });

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return services;
        }
    }
}
