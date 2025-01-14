using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttribute_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttribute_DeleteRelationsBehavior(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductSpecificationAttribute command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductSpecificationAttributeResponse = await next();

            if (deleteProductSpecificationAttributeResponse)
            {
                await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

                var product_ProductSpecificationAttribute_Mappings = await applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                    .ToListAsync();

                applicationDbContext.RemoveRange(product_ProductSpecificationAttribute_Mappings);

                try
                {
                    await applicationDbContext.SaveChangesAsync();
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
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = command.Identity.GetUserId(),
                EntityType = EntityType.PRODUCT_PRODUCTSPECIFICATIONATTRIBUTE,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(
                    LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, "-"),
            }));

            return deleteProductSpecificationAttributeResponse;
        }

        #endregion
    }
}
