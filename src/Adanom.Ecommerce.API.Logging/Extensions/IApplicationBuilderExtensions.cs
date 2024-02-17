using Adanom.Ecommerce.API.Logging.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationLogging(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ApplyMigrationsMiddleware>();

            return applicationBuilder;
        }
    }
}
