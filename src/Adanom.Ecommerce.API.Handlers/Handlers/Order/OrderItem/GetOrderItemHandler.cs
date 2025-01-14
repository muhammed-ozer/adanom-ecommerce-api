namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderItemHandler : IRequestHandler<GetOrderItem, OrderItemResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderItemHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderItemResponse?> Handle(GetOrderItem command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderItem = await applicationDbContext.OrderItems
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            var orderItemResponse = _mapper.Map<OrderItemResponse>(orderItem);

            return orderItemResponse;
        }

        #endregion
    }
}
