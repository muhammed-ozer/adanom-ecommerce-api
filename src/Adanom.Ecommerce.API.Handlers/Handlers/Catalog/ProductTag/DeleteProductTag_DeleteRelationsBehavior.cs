using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductTag_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductTag, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductTag_DeleteRelationsBehavior(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductTag command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductTagResponse = await next();

            if (deleteProductTagResponse)
            {
                var product_ProductTag_Mappings = await _applicationDbContext.Product_ProductTag_Mappings
                                .Where(e => e.ProductTagId == command.Id)
                                .ToListAsync();

                if (product_ProductTag_Mappings.Any())
                {
                    _applicationDbContext.RemoveRange(product_ProductTag_Mappings);

                    try
                    {
                        await _applicationDbContext.SaveChangesAsync();

                        await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                        {
                            UserId = command.Identity.GetUserId(),
                            EntityType = EntityType.PRODUCTTAG,
                            TransactionType = TransactionType.DELETE,
                            Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, "-"),
                        }));
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

            return deleteProductTagResponse;
        }

        #endregion
    }
}
