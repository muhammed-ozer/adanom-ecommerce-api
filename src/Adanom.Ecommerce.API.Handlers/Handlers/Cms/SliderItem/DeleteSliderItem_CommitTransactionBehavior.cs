using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteSliderItem_CommitTransactionBehavior : IPipelineBehavior<DeleteSliderItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteSliderItem_CommitTransactionBehavior(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteSliderItem command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteSliderItemResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (!deleteSliderItemResponse)
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);

                    await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                    {
                        UserId = command.Identity.GetUserId(),
                        EntityType = EntityType.SLIDERITEM,
                        TransactionType = TransactionType.DELETE,
                        Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                    }));
                }

                return false;
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
                    EntityType = EntityType.SLIDERITEM,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return deleteSliderItemResponse;
        }

        #endregion
    }
}
