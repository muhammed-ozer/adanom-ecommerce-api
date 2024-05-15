using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAddressCitiesHandler : IRequestHandler<GetAddressCities, IEnumerable<AddressCityResponse>>,
        INotificationHandler<ClearEntityCache<AddressCityResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, AddressCityResponse> _cache = new();

        #endregion

        #region Ctor

        public GetAddressCitiesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<AddressCityResponse>> Handle(GetAddressCities command, CancellationToken cancellationToken)
        {
            if (!_cache.Values.Any())
            {
                var addressCitiesOnDb = await _applicationDbContext.AddressCities
                   .AsNoTracking()
                   .ToListAsync();

                var addressCityResponses = _mapper.Map<IEnumerable<AddressCityResponse>>(addressCitiesOnDb);

                foreach (var item in addressCityResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var addressCities = _cache.Values
                .AsEnumerable()
                .OrderBy(e => e.Code);

            return addressCities;
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<AddressCityResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
