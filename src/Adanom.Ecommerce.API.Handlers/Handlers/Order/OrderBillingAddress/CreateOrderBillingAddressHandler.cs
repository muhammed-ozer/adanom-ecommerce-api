namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderBillingAddressHandler : IRequestHandler<CreateOrderBillingAddress, OrderBillingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrderBillingAddressHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderBillingAddressResponse?> Handle(CreateOrderBillingAddress command, CancellationToken cancellationToken)
        {
            var orderBillingAddress = _mapper.Map<CreateOrderBillingAddress, OrderBillingAddress>(command);

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(orderBillingAddress);
            await applicationDbContext.SaveChangesAsync();
            
            var orderBillingAddressResponse = _mapper.Map<OrderBillingAddressResponse>(orderBillingAddress);

            return orderBillingAddressResponse;
        }

        #endregion
    }
}
