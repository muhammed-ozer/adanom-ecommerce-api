namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShoppingCart_LastModifiedDateHandler : IRequestHandler<UpdateShoppingCart_LastModifiedDate, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShoppingCart_LastModifiedDateHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShoppingCart_LastModifiedDate command, CancellationToken cancellationToken)
        {
            var shoppingCart = await _applicationDbContext.ShoppingCarts
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            if (shoppingCart == null)
            {
                return true;
            }

            shoppingCart.LastModifiedAtUtc = DateTime.UtcNow;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.SHOPPINGCART,
                    TransactionType = TransactionType.UPDATE,
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
