namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplication(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApplicationData();

            applicationBuilder.UseApplicationServices();

            applicationBuilder.UseApplicationSecurity();

            applicationBuilder.UseApplicationCommands();

            applicationBuilder.UseApplicationValidations();

            applicationBuilder.UseApplicationHandlers();

            applicationBuilder.UseApplicationScheduler();

            applicationBuilder.UseApplicationGraphql();

            return applicationBuilder;
        }
    }
}
