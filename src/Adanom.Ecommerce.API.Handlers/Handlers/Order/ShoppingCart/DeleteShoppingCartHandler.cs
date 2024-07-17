using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShoppingCartHandler : IRequestHandler<DeleteShoppingCart, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteShoppingCart command, CancellationToken cancellationToken)
        {
            ShoppingCart? shoppingCart = null;

            if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                shoppingCart = await _applicationDbContext.ShoppingCarts
                    .Where(e => e.UserId == userId)
                    .SingleOrDefaultAsync();
            }
            else
            {
                shoppingCart = await _applicationDbContext.ShoppingCarts
                    .Where(e => e.Id == command.Id)
                    .SingleOrDefaultAsync();
            }

            if (shoppingCart == null)
            {
                return true;
            }

            _applicationDbContext.Remove(shoppingCart);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
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
