using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingProvidersHandler : IRequestHandler<GetShippingProviders, PaginatedData<ShippingProviderResponse>>,
        INotificationHandler<ClearEntityCache<ShippingProviderResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ShippingProviderResponse> _cache = new();

        #endregion

        #region Ctor

        public GetShippingProvidersHandler(
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

        public async Task<PaginatedData<ShippingProviderResponse>> Handle(GetShippingProviders command, CancellationToken cancellationToken)
        {
            var shippingProvidersCountOnDb = await _applicationDbContext.ShippingProviders
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || shippingProvidersCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var shippingProvidersOnDb = await _applicationDbContext.ShippingProviders
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var shippingProviderResponsesFromDb = _mapper
                    .Map<IEnumerable<ShippingProviderResponse>>(shippingProvidersOnDb);

                foreach (var item in shippingProviderResponsesFromDb)
                {
                    item.Logo = await _mediator.Send(new GetEntityImage(item.Id, EntityType.SHIPPINGPROVIDER, ImageType.LOGO));

                    _cache.TryAdd(item.Id, item);
                }
            }

            var shippingProviders = _cache.Values.OrderBy(e => e.IsDefault).ThenBy(e => e.DisplayName).AsEnumerable();

            var totalCount = shippingProviders.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                shippingProviders = shippingProviders
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            shippingProviders = shippingProviders.OrderBy(e => e.DisplayName);

            var shippingProviderResponses = _mapper.Map<IEnumerable<ShippingProviderResponse>>(shippingProviders);

            return new PaginatedData<ShippingProviderResponse>(
                shippingProviderResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<ShippingProviderResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
