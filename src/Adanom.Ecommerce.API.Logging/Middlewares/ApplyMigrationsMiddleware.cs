using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Logging.Middlewares
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

        public async Task InvokeAsync(HttpContext httpContext, LogDbContext logDbContext)
        {
            if (logDbContext is null)
            {
                throw new NullReferenceException(nameof(logDbContext));
            }

            await MigrateDatabaseAsync(logDbContext);

            await _next(httpContext);
        }

        #endregion

        #region MigrateDatabaseAsync

        private async Task MigrateDatabaseAsync(LogDbContext logDbContext)
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

            var pendingMigrations = await logDbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await logDbContext.Database.MigrateAsync();
            }

            _appliedMigrations = true;
            _lockSlim.Release(1);
        }

        #endregion
    }
}
