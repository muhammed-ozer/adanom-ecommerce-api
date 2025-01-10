namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetLocalDeliveryProvider_SupportedAddressDistrictsHandler :
        IRequestHandler<GetLocalDeliveryProvider_SupportedAddressDistricts, IEnumerable<AddressDistrictResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetLocalDeliveryProvider_SupportedAddressDistrictsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<AddressDistrictResponse>> Handle(GetLocalDeliveryProvider_SupportedAddressDistricts command, CancellationToken cancellationToken)
        {
            var addressDistrictsQuery = _applicationDbContext.LocalDeliveryProvider_AddressDistrict_Mappings
                .Where(e => e.LocalDeliveryProviderId == command.LocalDeliveryProviderId && e.LocalDeliveryProvider.DeletedAtUtc == null)
                .Select(e => e.AddressDistrict);

            var addressDistrictResponses = _mapper.Map<IEnumerable<AddressDistrictResponse>>(await addressDistrictsQuery.ToListAsync());

            return addressDistrictResponses;
        }

        #endregion
    }
}
