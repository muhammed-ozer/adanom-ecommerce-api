namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderItemsHandler : IRequestHandler<GetOrderItems, IEnumerable<OrderItemResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderItemsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<OrderItemResponse>> Handle(GetOrderItems command, CancellationToken cancellationToken)
        {
            var orderItems = await _applicationDbContext.OrderItems
                .AsNoTracking()
                .Where(e => e.OrderId == command.Filter.OrderId)
                .ToListAsync();

            var orderItemResponses = _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);

            return orderItemResponses;
        }

        #endregion
    }
}
