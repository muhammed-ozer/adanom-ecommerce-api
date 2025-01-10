using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetLocalDeliveryProvidersHandler : IRequestHandler<GetLocalDeliveryProviders, PaginatedData<LocalDeliveryProviderResponse>>,
        INotificationHandler<ClearEntityCache<LocalDeliveryProviderResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, LocalDeliveryProviderResponse> _cache = new();

        #endregion

        #region Ctor

        public GetLocalDeliveryProvidersHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<LocalDeliveryProviderResponse>> Handle(GetLocalDeliveryProviders command, CancellationToken cancellationToken)
        {
            var localDeliveryProvidersCountOnDb = await _applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || localDeliveryProvidersCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var localDeliveryProvidersOnDb = await _applicationDbContext.LocalDeliveryProviders
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var localDeliveryProviderResponsesFromDb = _mapper
                    .Map<IEnumerable<LocalDeliveryProviderResponse>>(localDeliveryProvidersOnDb);

                foreach (var item in localDeliveryProviderResponsesFromDb)
                {
                    item.Logo = await _mediator.Send(new GetEntityImage(item.Id, EntityType.LOCALDELIVERYPROVIDER, ImageType.LOGO));

                    _cache.TryAdd(item.Id, item);
                }
            }

            var localDeliveryProviders = _cache.Values.OrderBy(e => e.IsDefault).ThenBy(e => e.DisplayName).AsEnumerable();

            var totalCount = localDeliveryProviders.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                localDeliveryProviders = localDeliveryProviders
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            localDeliveryProviders = localDeliveryProviders.OrderBy(e => e.DisplayName);

            var localDeliveryProviderResponses = _mapper.Map<IEnumerable<LocalDeliveryProviderResponse>>(localDeliveryProviders);

            return new PaginatedData<LocalDeliveryProviderResponse>(
                localDeliveryProviderResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<LocalDeliveryProviderResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
