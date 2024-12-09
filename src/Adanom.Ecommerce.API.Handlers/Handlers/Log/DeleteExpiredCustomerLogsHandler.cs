namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredCustomerLogsHandler : INotificationHandler<DeleteExpiredCustomerLogs>
    {
        #region Fields

        private readonly ILogService _logService;

        #endregion

        #region Ctor

        public DeleteExpiredCustomerLogsHandler(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(DeleteExpiredCustomerLogs command, CancellationToken cancellationToken)
        {
            await _logService.DeleteExpiredCustomerLogsAsync();
        } 

        #endregion
    }
}
