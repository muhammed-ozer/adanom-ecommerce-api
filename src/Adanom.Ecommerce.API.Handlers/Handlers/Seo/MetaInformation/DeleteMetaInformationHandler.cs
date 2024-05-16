using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteMetaInformationHandler : IRequestHandler<DeleteMetaInformation, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteMetaInformationHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteMetaInformation command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var metaInformation = await _applicationDbContext.MetaInformations
                .Where(e => e.EntityId == command.EntityId &&
                            e.EntityType == command.EntityType)
                .SingleOrDefaultAsync();

            if (metaInformation == null)
            {
                return true;
            }

            _applicationDbContext.Remove(metaInformation);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.METAINFORMATION,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.METAINFORMATION,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, metaInformation.Id),
            }));

            return true;
        }

        #endregion
    }
}
