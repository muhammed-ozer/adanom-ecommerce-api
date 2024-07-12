using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderBillingAddressHandler : IRequestHandler<CreateOrderBillingAddress, OrderBillingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrderBillingAddressHandler(
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

        public async Task<OrderBillingAddressResponse?> Handle(CreateOrderBillingAddress command, CancellationToken cancellationToken)
        {
            var orderBillingAddress = _mapper.Map<CreateOrderBillingAddress, OrderBillingAddress>(command);

            await _applicationDbContext.AddAsync(orderBillingAddress);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            var orderBillingAddressResponse = _mapper.Map<OrderBillingAddressResponse>(orderBillingAddress);

            return orderBillingAddressResponse;
        }

        #endregion
    }
}
