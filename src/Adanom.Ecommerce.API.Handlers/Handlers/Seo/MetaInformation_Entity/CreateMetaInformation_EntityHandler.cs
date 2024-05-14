using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateMetaInformation_EntityHandler : IRequestHandler<CreateMetaInformation_Entity, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateMetaInformation_EntityHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateMetaInformation_Entity command, CancellationToken cancellationToken)
        {
            var metaInformation_Entity = new MetaInformation_Entity_Mapping()
            {
                MetaInformationId = command.MetaInformationId,
                EntityId = command.EntityId,
                EntityType = command.EntityType,
            };

            await _applicationDbContext.AddAsync(metaInformation_Entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.METAINFORMATION_ENTITY,
                    TransactionType = TransactionType.CREATE,
                    Description = string.Format(
                        LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                        $"{metaInformation_Entity.MetaInformationId}-{metaInformation_Entity.EntityId}-{metaInformation_Entity.EntityType}"),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.METAINFORMATION_ENTITY,
                    TransactionType = TransactionType.CREATE,
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
