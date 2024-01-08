namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationMailServices(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
