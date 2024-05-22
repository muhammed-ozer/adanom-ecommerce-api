namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressHandler : IRequestHandler<GetShippingAddress, ShippingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShippingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShippingAddressResponse?> Handle(GetShippingAddress command, CancellationToken cancellationToken)
        {
            ShippingAddress? shippingAddress = null;

            if (command.IncludeDeleted != null && command.IncludeDeleted.Value)
            {
                shippingAddress = await _applicationDbContext.ShippingAddresses
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                shippingAddress = await _applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ShippingAddressResponse>(shippingAddress);
        } 

        #endregion
    }
}
