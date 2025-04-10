﻿using Adanom.Ecommerce.API;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var uiClientConstantsSection = configuration.GetSection("UIClientConstants");
            var mailNotificationConstantsSection = configuration.GetSection("MailNotificationConstants");

            UIClientConstants.Store.BaseURL = uiClientConstantsSection.GetValue<string>("Store:BaseURL")!;
            UIClientConstants.Auth.BaseURL = uiClientConstantsSection.GetValue<string>("Auth:BaseURL")!;
            UIClientConstants.Admin.BaseURL = uiClientConstantsSection.GetValue<string>("Admin:BaseURL")!;

            MailNotificationConstants.Receivers.ReturnRequest = mailNotificationConstantsSection.GetValue<string>("Receivers:ReturnRequest")!;
            MailNotificationConstants.Receivers.Order = mailNotificationConstantsSection.GetValue<string>("Receivers:Order")!;

            services.AddApplicationData(configuration);

            services.AddApplicationLogging(configuration);

            services.AddApplicationServices(configuration);

            services.AddApplicationSecurity(configuration);

            services.AddApplicationCaching(configuration);

            services.AddApplicationCommands(configuration);

            services.AddApplicationValidations(configuration);

            services.AddApplicationHandlers(configuration);

            services.AddApplicationScheduler(configuration);

            services.AddApplicationGraphql(configuration);
            services.AddApplicationGraphqlDataLoader(configuration);
            services.AddApplicationGraphqlAdmin(configuration);
            services.AddApplicationGraphqlAuth(configuration);
            services.AddApplicationGraphqlStore(configuration);

            return services;
        }
    }
}
