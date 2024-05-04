using Adanom.Ecommerce.API.Handlers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMediatR(options =>
            {
                options.Lifetime = ServiceLifetime.Scoped;

                options.RegisterServicesFromAssemblies(typeof(LoginHandler).Assembly, typeof(Login).Assembly);

                #region Brand

                options.AddBehavior<IPipelineBehavior<DeleteBrand, bool>, DeleteBrand_DeleteRelationsBehavior>();

                #endregion

                #region RegisterUser

                options.AddBehavior<IPipelineBehavior<RegisterUser, bool>, RegisterUser_SendMailsBehavior>();
                options.AddBehavior<IPipelineBehavior<RegisterUser, bool>, RegisterUser_CreateNotificationBehavior>();

                #endregion
            });

            return services;
        }
    }
}
