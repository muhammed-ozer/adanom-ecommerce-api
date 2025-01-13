using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductTag_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductTag, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory   ;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductTag_DeleteRelationsBehavior(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductTag command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductTagResponse = await next();

            if (deleteProductTagResponse)
            {
                await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

                var product_ProductTag_Mappings = await applicationDbContext.Product_ProductTag_Mappings
                                .Where(e => e.ProductTagId == command.Id)
                                .ToListAsync();

                if (product_ProductTag_Mappings.Any())
                {
                    applicationDbContext.RemoveRange(product_ProductTag_Mappings);

                    try
                    {
                        await applicationDbContext.SaveChangesAsync();
                    }
                    catch (Exception exception)
                    {
                        await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                        {
                            UserId = command.Identity.GetUserId(),
                            EntityType = EntityType.PRODUCTTAG,
                            TransactionType = TransactionType.DELETE,
                            Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                            Exception = exception.ToString()
                        }));

                        return false;
                    }
                }
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = command.Identity.GetUserId(),
                EntityType = EntityType.PRODUCTTAG,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, "-"),
            }));

            return deleteProductTagResponse;
        }

        #endregion
    }
}
