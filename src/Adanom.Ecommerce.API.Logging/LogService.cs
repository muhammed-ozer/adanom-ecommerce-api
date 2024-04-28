using Adanom.Ecommerce.API.Logging.Models;

namespace Adanom.Ecommerce.API.Logging
{
    internal sealed class LogService : ILogService
    {
        #region Fields

        private readonly LogDbContext _logDbContext;

        #endregion

        #region Ctor

        public LogService(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext ?? throw new ArgumentNullException(nameof(logDbContext));
        }

        #endregion

        #region ILogService Members

        public async Task CreateAsync(AuthLogRequest request)
        {
            var log = new AuthLog()
            {
                LogLevel = request.LogLevel,
                UserEmail = request.UserEmail,
                Description = request.Description,
                CreatedAtUtc = DateTime.UtcNow,
            };

            await _logDbContext.AuthLogs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
