namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationServices(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApplicationMailServices();
            applicationBuilder.UseApplicationAzureServices();

            return applicationBuilder;
        }
    }
}
