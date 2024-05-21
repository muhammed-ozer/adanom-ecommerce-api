namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderItemHandler : IRequestHandler<GetOrderItem, OrderItemResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderItemHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderItemResponse?> Handle(GetOrderItem command, CancellationToken cancellationToken)
        {
            var orderItem = await _applicationDbContext.OrderItems
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            var orderItemResponse = _mapper.Map<OrderItemResponse>(orderItem);

            return orderItemResponse;
        }

        #endregion
    }
}
