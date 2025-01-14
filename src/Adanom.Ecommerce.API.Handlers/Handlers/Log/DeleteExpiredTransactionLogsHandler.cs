namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredTransactionLogsHandler : INotificationHandler<DeleteExpiredTransactionLogs>
    {
        #region Fields

        private readonly ILogService _logService;

        #endregion

        #region Ctor

        public DeleteExpiredTransactionLogsHandler(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(DeleteExpiredTransactionLogs command, CancellationToken cancellationToken)
        {
            await _logService.DeleteExpiredTransactionLogsAsync();
        } 

        #endregion
    }
}
