namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredAnonymousShoppingCartsHandler : IRequestHandler<DeleteExpiredAnonymousShoppingCarts, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteExpiredAnonymousShoppingCartsHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteExpiredAnonymousShoppingCarts command, CancellationToken cancellationToken)
        {
            var currentExpirationTimeUtc = DateTime.UtcNow.AddDays(7 * -1);

            var anonymousShoppingCarts = await _applicationDbContext.AnonymousShoppingCarts
                .Where(e => e.LastModifiedAtUtc <= currentExpirationTimeUtc)
                .ToListAsync();

            _applicationDbContext.AnonymousShoppingCarts.RemoveRange(anonymousShoppingCarts);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
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
