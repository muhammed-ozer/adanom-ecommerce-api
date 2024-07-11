using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCompanyHandler : IRequestHandler<GetCompany, CompanyResponse?>,
        INotificationHandler<ClearEntityCache<CompanyResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, CompanyResponse> _cache = new();

        #endregion

        #region Ctor

        public GetCompanyHandler(
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

        public async Task<CompanyResponse?> Handle(GetCompany command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return _cache.Values.FirstOrDefault();
            }

            var company = await _applicationDbContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var companyResponse = _mapper.Map<CompanyResponse>(company);

            if (companyResponse != null)
            {
                companyResponse.AddressCity = await _mediator.Send(new GetAddressCity(companyResponse.AddressCityId));
                companyResponse.AddressDistrict = await _mediator.Send(new GetAddressDistrict(companyResponse.AddressDistrictId));

                _cache.TryAdd(companyResponse.Id, companyResponse);
            }

            return companyResponse;
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<CompanyResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        #endregion
    }
}
