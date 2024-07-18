using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderHandler : IRequestHandler<GetOrder, OrderResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderResponse?> Handle(GetOrder command, CancellationToken cancellationToken)
        {
            var ordersQuery = _applicationDbContext.Orders.AsNoTracking();

            if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                ordersQuery = ordersQuery.Where(e => e.UserId == userId);
            }

            Order? order;

            if (command.OrderNumber.IsNotNullOrEmpty())
            {
                order = await ordersQuery.Where(e => e.OrderNumber == command.OrderNumber)
                                         .SingleOrDefaultAsync();
            }
            else
            {
                order = await ordersQuery.Where(e => e.Id == command.Id)
                                         .SingleOrDefaultAsync();
            }

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }

        #endregion
    }
}
