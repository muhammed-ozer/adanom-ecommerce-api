using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CommitTransactionBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_CommitTransactionBehavior(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductResponse?> Handle(CreateProduct command, RequestHandlerDelegate<ProductResponse?> next, CancellationToken cancellationToken)
        {
            var productResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (productResponse == null)
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);

                    await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                    {
                        UserId = command.Identity.GetUserId(),
                        EntityType = EntityType.PRODUCT,
                        TransactionType = TransactionType.CREATE,
                        Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                    }));
                }

                return null;
            }

            try
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.CommitAsync();
                }
            }
            catch (Exception exception)
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.PRODUCT,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                    Exception = exception.ToString()
                }));

                productResponse = null;
            }

            return productResponse;
        }

        #endregion
    }
}
