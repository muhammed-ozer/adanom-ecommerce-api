using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateStockNotificationItemHandler : IRequestHandler<CreateStockNotificationItem, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateStockNotificationItemHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateStockNotificationItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var stockNotificationItem = _mapper.Map<CreateStockNotificationItem, StockNotificationItem>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(stockNotificationItem);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
