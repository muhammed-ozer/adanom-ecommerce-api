using Adanom.Ecommerce.API.Scheduler.Jobs;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(options =>
            {
                //options.UseMicrosoftDependencyInjectionJobFactory();
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();

                options.AddJob<DeleteExpiredAnonymousShoppingCartsJob>(e => 
                    e.WithIdentity("DeleteExpiredAnonymousShoppingCartsJob", "AnonymousShoppingCartsGroup"));

                options.AddTrigger(e =>
                    e.WithIdentity("DeleteExpiredAnonymousShoppingCartsTrigger", "AnonymousShoppingCartsGroup")
                       .WithCronSchedule("0 30 1 ? * WED *")
                       .ForJob("DeleteExpiredAnonymousShoppingCartsJob", "AnonymousShoppingCartsGroup"));
            });

            // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            return services;
        }
    }
}
