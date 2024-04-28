using Adanom.Ecommerce.API.Logging;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateLogHandler : INotificationHandler<CreateLog>
    {
        #region Fields

        private readonly ILogService _logService;

        #endregion

        #region Ctor

        public CreateLogHandler(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(CreateLog command, CancellationToken cancellationToken)
        {
            switch (command.Request)
            {
                case AuthLogRequest request:
                    await _logService.CreateAsync(request);
                    break;
                default:
                    break;
            }
        } 

        #endregion
    }
}
