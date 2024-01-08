namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationAzureServices(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
