namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteAnonymousShoppingCartHandler : IRequestHandler<DeleteAnonymousShoppingCart, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteAnonymousShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCart = await _applicationDbContext.AnonymousShoppingCarts
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            if (anonymousShoppingCart == null)
            {
                return true;
            }

            _applicationDbContext.Remove(anonymousShoppingCart);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.ANONYMOUSSHOPPINGCART,
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
