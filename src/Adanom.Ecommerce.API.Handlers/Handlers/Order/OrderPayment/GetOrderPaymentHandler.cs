namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderPaymentHandler : IRequestHandler<GetOrderPayment, OrderPaymentResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderPaymentHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderPaymentResponse?> Handle(GetOrderPayment command, CancellationToken cancellationToken)
        {
            OrderPayment? orderPayment;

            if (command.OrderNumber.IsNotNullOrEmpty())
            {
                orderPayment = await _applicationDbContext.OrderPayments
                    .AsNoTracking()
                    .Where(e => e.Order.OrderNumber == command.OrderNumber)
                    .SingleOrDefaultAsync();
            }
            else
            {
                orderPayment = await _applicationDbContext.OrderPayments
                    .AsNoTracking()
                    .Where(e => e.Id == command.Id)
                    .SingleOrDefaultAsync();
            }

            var orderPaymentResponse = _mapper.Map<OrderPaymentResponse>(orderPayment);

            return orderPaymentResponse;
        }

        #endregion
    }
}
