using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShoppingCartHandler : IRequestHandler<DeleteShoppingCart, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteShoppingCartHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteShoppingCart command, CancellationToken cancellationToken)
        {
            ShoppingCart? shoppingCart = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                shoppingCart = await applicationDbContext.ShoppingCarts
                    .Where(e => e.UserId == userId)
                    .SingleOrDefaultAsync();
            }
            else
            {
                shoppingCart = await applicationDbContext.ShoppingCarts
                    .Where(e => e.Id == command.Id)
                    .SingleOrDefaultAsync();
            }

            if (shoppingCart == null)
            {
                return true;
            }

            applicationDbContext.Remove(shoppingCart);

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.SHOPPINGCART,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
