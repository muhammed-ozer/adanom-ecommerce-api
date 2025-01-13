namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressHandler : IRequestHandler<GetShippingAddress, ShippingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShippingAddressHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShippingAddressResponse?> Handle(GetShippingAddress command, CancellationToken cancellationToken)
        {
            ShippingAddress? shippingAddress = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.IncludeDeleted != null && command.IncludeDeleted.Value)
            {
                shippingAddress = await applicationDbContext.ShippingAddresses
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                shippingAddress = await applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ShippingAddressResponse>(shippingAddress);
        }

        #endregion
    }
}
