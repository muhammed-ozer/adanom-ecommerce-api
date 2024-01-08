namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationCommands(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
