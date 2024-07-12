using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderShippingAddressHandler : IRequestHandler<CreateOrderShippingAddress, OrderShippingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrderShippingAddressHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderShippingAddressResponse?> Handle(CreateOrderShippingAddress command, CancellationToken cancellationToken)
        {
            var orderShippingAddress = _mapper.Map<CreateOrderShippingAddress, OrderShippingAddress>(command);

            await _applicationDbContext.AddAsync(orderShippingAddress);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            var orderShippingAddressResponse = _mapper.Map<OrderShippingAddressResponse>(orderShippingAddress);

            return orderShippingAddressResponse;
        }

        #endregion
    }
}
