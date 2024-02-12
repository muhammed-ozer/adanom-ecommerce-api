namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
