using Adanom.Ecommerce.API.Commands;
using Adanom.Ecommerce.API.Handlers;
using MediatR;
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

                options.RegisterServicesFromAssemblies(typeof(TestHandler).Assembly, typeof(Test).Assembly);
            });

            return services;
        }
    }
}
