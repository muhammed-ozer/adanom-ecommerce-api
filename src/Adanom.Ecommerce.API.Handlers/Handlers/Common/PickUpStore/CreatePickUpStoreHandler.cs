using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreatePickUpStoreHandler : IRequestHandler<CreatePickUpStore, PickUpStoreResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreatePickUpStoreHandler(
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

        public async Task<PickUpStoreResponse?> Handle(CreatePickUpStore command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!command.IsDefault)
            {
                var hasAnyOtherPickUpStore = await applicationDbContext.PickUpStores
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null);

                if (!hasAnyOtherPickUpStore)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultPickUpStore = await applicationDbContext.PickUpStores
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultPickUpStore != null)
                {
                    currentDefaultPickUpStore.IsDefault = false;
                }
            }

            var pickUpStore = _mapper.Map<CreatePickUpStore, PickUpStore>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await applicationDbContext.AddAsync(pickUpStore);
            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            var pickUpStoreResponse = _mapper.Map<PickUpStoreResponse>(pickUpStore);

            return pickUpStoreResponse;
        }

        #endregion
    }
}
