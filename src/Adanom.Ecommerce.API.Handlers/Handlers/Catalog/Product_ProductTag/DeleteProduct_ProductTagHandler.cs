using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductTagHandler : IRequestHandler<DeleteProduct_ProductTag, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductTagHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductTag command, CancellationToken cancellationToken)
        {
            var product_ProductTag = await _applicationDbContext.Product_ProductTag_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductTagId == command.ProductTagId)
                .SingleOrDefaultAsync();

            if (product_ProductTag == null)
            {
                return true;
            }

            _applicationDbContext.Remove(product_ProductTag);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTTAG,
                    TransactionType = TransactionType.DELETE,
                    Description = string.Format(
                        LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                        $"{product_ProductTag.ProductId}-{product_ProductTag.ProductTagId}"),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTTAG,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
