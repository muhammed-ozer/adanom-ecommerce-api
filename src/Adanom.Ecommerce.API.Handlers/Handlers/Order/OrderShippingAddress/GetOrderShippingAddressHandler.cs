namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderShippingAddressHandler : IRequestHandler<GetOrderShippingAddress, OrderShippingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderShippingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderShippingAddressResponse?> Handle(GetOrderShippingAddress command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderShippingAddress = await applicationDbContext.OrderShippingAddresses
                .Where(e => e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<OrderShippingAddressResponse>(orderShippingAddress);
        }

        #endregion
    }
}
