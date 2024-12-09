namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredAdminLogsHandler : INotificationHandler<DeleteExpiredAdminLogs>
    {
        #region Fields

        private readonly ILogService _logService;

        #endregion

        #region Ctor

        public DeleteExpiredAdminLogsHandler(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(DeleteExpiredAdminLogs command, CancellationToken cancellationToken)
        {
            await _logService.DeleteExpiredAdminLogsAsync();
        } 

        #endregion
    }
}
