using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderHandler : IRequestHandler<GetOrder, OrderResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderResponse?> Handle(GetOrder command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var ordersQuery = applicationDbContext.Orders.AsNoTracking();

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
