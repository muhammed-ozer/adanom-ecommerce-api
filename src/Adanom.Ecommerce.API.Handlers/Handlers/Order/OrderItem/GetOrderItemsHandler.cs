namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderItemsHandler : IRequestHandler<GetOrderItems, IEnumerable<OrderItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderItemsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<OrderItemResponse>> Handle(GetOrderItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderItems = await applicationDbContext.OrderItems
                .AsNoTracking()
                .Where(e => e.OrderId == command.Filter.OrderId)
                .ToListAsync();

            var orderItemResponses = _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);

            return orderItemResponses;
        }

        #endregion
    }
}
