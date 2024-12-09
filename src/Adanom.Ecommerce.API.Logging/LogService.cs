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
                case AdminTransactionLogRequest adminTransactionLogRequest:
                    await CreateAdminTransactionLogAsync(adminTransactionLogRequest);
                    break;
                case CustomerTransactionLogRequest customerTransactionLogRequest:
                    await CreateCustomerTransactionLogAsync(customerTransactionLogRequest);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region DeleteExpiredAdminLogsAsync

        public async Task DeleteExpiredAdminLogsAsync()
        {
            var currentExpirationTimeUtc = DateTime.UtcNow.AddMonths(3 * -1);

            try
            {
                await _logDbContext.AdminTransactionLogs
                    .Where(e => e.CreatedAtUtc <= currentExpirationTimeUtc)
                    .ExecuteDeleteAsync();
            }
            catch
            {
            }
        }

        #endregion

        #region DeleteExpiredCustomerLogsAsync

        public async Task DeleteExpiredCustomerLogsAsync()
        {
            var currentExpirationTimeUtc = DateTime.UtcNow.AddYears(1 * -1);

            try
            {
                await _logDbContext.CustomerTransactionLogs
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

        #region CreateAdminTransactionLogAsync

        private async Task CreateAdminTransactionLogAsync(AdminTransactionLogRequest request)
        {
            var log = new AdminTransactionLog()
            {
                LogLevel = request.LogLevel,
                EntityType = request.EntityType,
                TransactionType = request.TransactionType,
                UserId = request.UserId,
                Description = request.Description,
                Exception = request.Exception,
                CreatedAtUtc = DateTime.UtcNow,
            };

            await _logDbContext.AdminTransactionLogs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
        }

        #endregion

        #region CreateCustomerTransactionLogAsync

        private async Task CreateCustomerTransactionLogAsync(CustomerTransactionLogRequest request)
        {
            var log = new CustomerTransactionLog()
            {
                LogLevel = request.LogLevel,
                EntityType = request.EntityType,
                TransactionType = request.TransactionType,
                UserId = request.UserId,
                Description = request.Description,
                Exception = request.Exception,
                CreatedAtUtc = DateTime.UtcNow,
            };

            await _logDbContext.CustomerTransactionLogs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}
