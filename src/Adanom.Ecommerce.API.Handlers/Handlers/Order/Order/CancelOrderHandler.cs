using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CancelOrderHandler : IRequestHandler<CancelOrder, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public CancelOrderHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CancelOrder command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userId = command.Identity.GetUserId();

            var order = await applicationDbContext.Orders
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            order.OrderStatusType = OrderStatusType.CANCEL;

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
