using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAddressDistrictsHandler : IRequestHandler<GetAddressDistricts, IEnumerable<AddressDistrictResponse>>,
        INotificationHandler<ClearEntityCache<AddressDistrictResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, AddressDistrictResponse> _cache = new();

        #endregion

        #region Ctor

        public GetAddressDistrictsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<AddressDistrictResponse>> Handle(GetAddressDistricts command, CancellationToken cancellationToken)
        {
            if (!_cache.Values.Any())
            {
                var addressDistrictsOnDb = await _applicationDbContext.AddressDistricts
                   .AsNoTracking()
                   .ToListAsync();

                var addressDistrictResponses = _mapper.Map<IEnumerable<AddressDistrictResponse>>(addressDistrictsOnDb);

                foreach (var item in addressDistrictResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var addressDistricts = _cache.Values.AsEnumerable();

            if (command.AddressCityId != null)
            {
                addressDistricts = addressDistricts
                    .Where(e => e.AddressCityId == command.AddressCityId.Value);
            }

            addressDistricts = addressDistricts
                .OrderBy(e => e.AddressCityId)
                .ThenBy(e => e.Name);

            return addressDistricts;
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<AddressDistrictResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
