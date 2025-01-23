using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeHandler : IRequestHandler<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateOrder_OrderStatusType command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var order = await applicationDbContext.Orders
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            command.OldOrderStatusType = order.OrderStatusType;

            if (command.NewOrderStatusType == OrderStatusType.DONE)
            {
                if (command.DeliveredAtUtc == null)
                {
                    command.DeliveredAtUtc = DateTime.UtcNow;
                }
            }

            order = _mapper.Map(command, order);

            order.UpdatedAtUtc = DateTime.UtcNow;
            order.UpdatedByUserId = userId;

            applicationDbContext.Update(order);

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
