using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductPriceHandler : IRequestHandler<DeleteProductPrice, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductPriceHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductPrice command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productPrice = await _applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            // Check productSKU has deleted before price delete
            var productSKUsThatUseProductPrice = await _applicationDbContext.ProductSKUs
                        .Where(e => e.DeletedAtUtc == null && e.ProductPriceId == productPrice.Id)
                        .ToListAsync();

            if (productSKUsThatUseProductPrice.Any())
            {
                return false;
            }

            productPrice.DeletedAtUtc = DateTime.UtcNow;
            productPrice.DeletedByUserId = userId;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PRODUCTPRICE,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PRODUCTPRICE,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, productPrice.Id),
            }));

            return true;
        }

        #endregion
    }
}
