namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderPaymentHandler : IRequestHandler<GetOrderPayment, OrderPaymentResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderPaymentHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderPaymentResponse?> Handle(GetOrderPayment command, CancellationToken cancellationToken)
        {
            OrderPayment? orderPayment;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.OrderNumber.IsNotNullOrEmpty())
            {
                orderPayment = await applicationDbContext.OrderPayments
                    .AsNoTracking()
                    .Where(e => e.Order.OrderNumber == command.OrderNumber)
                    .SingleOrDefaultAsync();
            }
            else
            {
                orderPayment = await applicationDbContext.OrderPayments
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
