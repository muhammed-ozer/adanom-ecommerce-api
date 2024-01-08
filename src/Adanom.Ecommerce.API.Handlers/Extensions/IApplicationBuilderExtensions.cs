namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationHandlers(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
