using Adanom.Ecommerce.API.Security.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationSecurity(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CreateClientMiddleware>();
            applicationBuilder.UseMiddleware<CreateScopesMiddleware>();

            applicationBuilder.UseMiddleware<SeedDataMiddleware>();

            return applicationBuilder;
        }
    }
}
