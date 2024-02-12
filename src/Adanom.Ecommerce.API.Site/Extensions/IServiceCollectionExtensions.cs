namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationData(configuration);

            services.AddApplicationLogging(configuration);

            services.AddApplicationServices(configuration);

            services.AddApplicationSecurity(configuration);

            services.AddApplicationCommands(configuration);

            services.AddApplicationValidations(configuration);

            services.AddApplicationHandlers(configuration);

            services.AddApplicationScheduler(configuration);

            services.AddApplicationGraphql(configuration);
            services.AddApplicationGraphqlBackend(configuration);
            services.AddApplicationGraphqlFrontend(configuration);

            return services;
        }
    }
}
