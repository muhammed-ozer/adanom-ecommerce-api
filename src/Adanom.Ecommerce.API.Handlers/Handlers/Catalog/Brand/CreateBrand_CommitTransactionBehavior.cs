using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateBrand_CommitTransactionBehavior : IPipelineBehavior<CreateBrand, BrandResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateBrand_CommitTransactionBehavior(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<BrandResponse?> Handle(CreateBrand command, RequestHandlerDelegate<BrandResponse?> next, CancellationToken cancellationToken)
        {
            var brandResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (brandResponse == null)
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);

                    await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                    {
                        UserId = command.Identity.GetUserId(),
                        EntityType = EntityType.BRAND,
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
                    EntityType = EntityType.BRAND,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                    Exception = exception.ToString()
                }));

                brandResponse = null;
            }

            return brandResponse;
        }

        #endregion
    }
}
