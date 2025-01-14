using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderShippingAddressHandler : IRequestHandler<CreateOrderShippingAddress, OrderShippingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrderShippingAddressHandler(
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

        public async Task<OrderShippingAddressResponse?> Handle(CreateOrderShippingAddress command, CancellationToken cancellationToken)
        {
            var orderShippingAddress = _mapper.Map<CreateOrderShippingAddress, OrderShippingAddress>(command);

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(orderShippingAddress);
            await applicationDbContext.SaveChangesAsync();

            var orderShippingAddressResponse = _mapper.Map<OrderShippingAddressResponse>(orderShippingAddress);

            return orderShippingAddressResponse;
        }

        #endregion
    }
}
