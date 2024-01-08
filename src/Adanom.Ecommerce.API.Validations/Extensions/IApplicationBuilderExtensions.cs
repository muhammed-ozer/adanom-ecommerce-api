namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationValidations(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
