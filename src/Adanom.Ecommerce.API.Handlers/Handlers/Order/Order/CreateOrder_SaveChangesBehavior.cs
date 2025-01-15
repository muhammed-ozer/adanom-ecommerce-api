namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_SaveChangesBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrder_SaveChangesBehavior(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<OrderResponse?> Handle(CreateOrder command, RequestHandlerDelegate<OrderResponse?> next, CancellationToken cancellationToken)
        {
            var orderResponse = await next();

            if (orderResponse == null)
            {
                return null;
            }

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var order = _mapper.Map<Order>(orderResponse);

            await applicationDbContext.AddAsync(order);
            await applicationDbContext.SaveChangesAsync();

            orderResponse.Id = order.Id;

            return orderResponse;
        }

        #endregion
    }
}
