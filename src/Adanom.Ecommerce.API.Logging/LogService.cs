using Adanom.Ecommerce.API.Logging.Models;
using Microsoft.EntityFrameworkCore;

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

        #region CreateAsync

        public async Task CreateAsync(BaseLogRequest request)
        {
            switch (request)
            {
                case AuthLogRequest authLogRequest:
                    await CreateAuthLogAsync(authLogRequest);
                    break;
                case TransactionLogRequest transactionLogRequest:
                    await CreateTransactionLogAsync(transactionLogRequest);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region DeleteExpiredTransactionLogsAsync

        public async Task DeleteExpiredTransactionLogsAsync()
        {
            var currentExpirationTimeUtc = DateTime.UtcNow.AddMonths(3 * -1);

            try
            {
                await _logDbContext.TransactionLogs
                    .Where(e => e.CreatedAtUtc <= currentExpirationTimeUtc)
                    .ExecuteDeleteAsync();
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region Private Methods

        #region CreateAuthLogAsync

        private async Task CreateAuthLogAsync(AuthLogRequest request)
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

        #region CreateTransactionLogAsync

        private async Task CreateTransactionLogAsync(TransactionLogRequest request)
        {
            var log = new TransactionLog()
            {
                LogLevel = request.LogLevel,
                Exception = request.Exception,
                CommandName = request.CommandName,
                CommandValues = request.CommandValues,
                UserId = request.UserId,
                CreatedAtUtc = DateTime.UtcNow,
            };

            await _logDbContext.TransactionLogs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}
