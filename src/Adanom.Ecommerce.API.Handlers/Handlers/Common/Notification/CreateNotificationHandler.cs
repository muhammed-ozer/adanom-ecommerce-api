namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateNotificationHandler : INotificationHandler<CreateNotification>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateNotificationHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
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

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(notification);
            await applicationDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
