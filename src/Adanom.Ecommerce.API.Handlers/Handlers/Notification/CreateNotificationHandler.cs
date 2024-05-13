namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateNotificationHandler : INotificationHandler<CreateNotification>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateNotificationHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(CreateNotification command, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<CreateNotification, Notification>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(notification);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.NOTIFICATION,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                }));
            }
        } 

        #endregion
    }
}
