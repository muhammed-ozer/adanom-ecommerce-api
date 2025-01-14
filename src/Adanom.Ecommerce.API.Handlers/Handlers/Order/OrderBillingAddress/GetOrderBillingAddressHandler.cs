namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderBillingAddressHandler : IRequestHandler<GetOrderBillingAddress, OrderBillingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderBillingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderBillingAddressResponse?> Handle(GetOrderBillingAddress command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderBillingAddress = await applicationDbContext.OrderBillingAddresses
                .Where(e => e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<OrderBillingAddressResponse>(orderBillingAddress);
        }

        #endregion
    }
}
