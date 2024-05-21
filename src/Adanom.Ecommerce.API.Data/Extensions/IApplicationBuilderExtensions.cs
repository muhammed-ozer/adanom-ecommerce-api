using Adanom.Ecommerce.API.Data.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationData(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ApplyMigrationsMiddleware>();
            applicationBuilder.UseMiddleware<SeedDataMiddleware>();

            return applicationBuilder;
        }
    }
}
