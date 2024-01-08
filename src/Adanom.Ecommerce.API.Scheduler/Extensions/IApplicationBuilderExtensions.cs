namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationScheduler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
