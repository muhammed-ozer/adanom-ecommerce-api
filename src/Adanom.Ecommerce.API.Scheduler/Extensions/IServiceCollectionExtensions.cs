﻿using Adanom.Ecommerce.API.Scheduler.Jobs;
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

                #region DeleteExpiredAnonymousShoppingCartsJob

                options.AddJob<DeleteExpiredAnonymousShoppingCartsJob>(e =>
                           e.WithIdentity("DeleteExpiredAnonymousShoppingCartsJob", "AnonymousShoppingCartsGroup"));

                options.AddTrigger(e =>
                    e.WithIdentity("DeleteExpiredAnonymousShoppingCartsTrigger", "AnonymousShoppingCartsGroup")
                       .WithCronSchedule("0 30 1 ? * WED *") // Every wednesdays 1:30am
                       .ForJob("DeleteExpiredAnonymousShoppingCartsJob", "AnonymousShoppingCartsGroup"));

                #endregion

                #region DeleteReadNotificationsJob

                options.AddJob<DeleteReadNotificationsJob>(e =>
                            e.WithIdentity("DeleteReadNotificationsJob", "NotificationsGroup"));

                options.AddTrigger(e =>
                    e.WithIdentity("DeleteReadNotificationsTrigger", "NotificationsGroup")
                       .WithCronSchedule("0 30 1 ? * TUE *") // Every tuesdays 1:30am
                       .ForJob("DeleteReadNotificationsJob", "NotificationsGroup"));

                #endregion

                #region DeleteExpiredTransactionLogsJob

                options.AddJob<DeleteExpiredTransactionLogsJob>(e =>
                           e.WithIdentity("DeleteExpiredTransactionLogsJob", "TransactionLogsGroup"));

                options.AddTrigger(e =>
                    e.WithIdentity("DeleteExpiredTransactionLogsTrigger", "TransactionLogsGroup")
                       .WithCronSchedule("0 0 2 5 1/1 ? *") // Every 5th day of every month 2:00am
                       .ForJob("DeleteExpiredTransactionLogsJob", "TransactionLogsGroup"));

                #endregion

                #region DeleteExpiredStockReservationsJob

                options.AddJob<DeleteExpiredStockReservationsJob>(e =>
                           e.WithIdentity("DeleteExpiredStockReservationsJob", "StockReservationsGroup"));

                options.AddTrigger(e =>
                    e.WithIdentity("DeleteExpiredStockReservationsTrigger", "StockReservationsGroup")
                       .WithCronSchedule("0 0 */4 * * ? *") // Every 4 hour
                       .ForJob("DeleteExpiredStockReservationsJob", "StockReservationsGroup"));

                #endregion
            });

            // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            return services;
        }
    }
}
