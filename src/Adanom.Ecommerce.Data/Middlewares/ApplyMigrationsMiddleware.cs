using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.Data.Middlewares
{
    internal sealed class ApplyMigrationsMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _lockSlim = new SemaphoreSlim(1, 1);
        private bool _appliedMigrations = false;

        #endregion

        #region Ctor

        public ApplyMigrationsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region InvokeAsync

        public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new NullReferenceException(nameof(dbContext));
            }

            await MigrateDatabaseAsync(dbContext);

            await _next(httpContext);
        }

        #endregion

        #region MigrateDatabaseAsync

        private async Task MigrateDatabaseAsync(ApplicationDbContext dbContext)
        {
            if (_appliedMigrations)
            {
                return;
            }

            await _lockSlim.WaitAsync();

            if (_appliedMigrations)
            {
                _lockSlim.Release(1);

                return;
            }

            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            _appliedMigrations = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
