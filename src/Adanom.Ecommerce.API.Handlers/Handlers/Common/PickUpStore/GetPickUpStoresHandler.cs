using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetPickUpStoresHandler : IRequestHandler<GetPickUpStores, PaginatedData<PickUpStoreResponse>>,
        INotificationHandler<ClearEntityCache<PickUpStoreResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, PickUpStoreResponse> _cache = new();

        #endregion

        #region Ctor

        public GetPickUpStoresHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<PickUpStoreResponse>> Handle(GetPickUpStores command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var pickUpStoresCountOnDb = await applicationDbContext.PickUpStores
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || pickUpStoresCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var pickUpStoresOnDb = await applicationDbContext.PickUpStores
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var pickUpStoreResponsesFromDb = _mapper
                    .Map<IEnumerable<PickUpStoreResponse>>(pickUpStoresOnDb);

                foreach (var item in pickUpStoreResponsesFromDb)
                {
                    item.Logo = await _mediator.Send(new GetEntityImage(item.Id, EntityType.PICKUPSTORE, ImageType.LOGO));

                    _cache.TryAdd(item.Id, item);
                }
            }

            var pickUpStores = _cache.Values.OrderBy(e => e.IsDefault).ThenBy(e => e.DisplayName).AsEnumerable();

            var totalCount = pickUpStores.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                pickUpStores = pickUpStores
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            pickUpStores = pickUpStores.OrderBy(e => e.DisplayName);

            var pickUpStoreResponses = _mapper.Map<IEnumerable<PickUpStoreResponse>>(pickUpStores);

            return new PaginatedData<PickUpStoreResponse>(
                pickUpStoreResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<PickUpStoreResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
