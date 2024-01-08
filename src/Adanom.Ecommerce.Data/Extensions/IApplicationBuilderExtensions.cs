using Adanom.Ecommerce.Data.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationData(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ApplyMigrationsMiddleware>();

            return applicationBuilder;
        }
    }
}
