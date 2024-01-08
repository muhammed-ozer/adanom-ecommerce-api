using Adanom.Ecommerce.Services.Mail;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}
