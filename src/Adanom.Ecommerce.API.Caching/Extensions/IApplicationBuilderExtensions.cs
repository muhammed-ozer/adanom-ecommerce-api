namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationCaching(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
