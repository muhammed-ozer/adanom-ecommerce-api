using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductSpecificationAttributeHandler : IRequestHandler<DeleteProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductSpecificationAttributeHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttribute = await _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSpecificationAttributeId == command.ProductSpecificationAttributeId)
                .SingleAsync();

            _applicationDbContext.Remove(product_ProductSpecificationAttribute);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
                    TransactionType = TransactionType.DELETE,
                    Description = string.Format(
                        LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                        $"{product_ProductSpecificationAttribute.ProductId}-{product_ProductSpecificationAttribute.ProductSpecificationAttributeId}"),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
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
