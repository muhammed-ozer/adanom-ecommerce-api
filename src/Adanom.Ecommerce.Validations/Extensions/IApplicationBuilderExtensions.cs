namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationValidation(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
