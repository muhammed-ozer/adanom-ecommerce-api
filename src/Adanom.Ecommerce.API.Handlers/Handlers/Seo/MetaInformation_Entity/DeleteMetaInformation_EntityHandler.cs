using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteMetaInformation_EntityHandler : IRequestHandler<DeleteMetaInformation_Entity, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteMetaInformation_EntityHandler(ApplicationDbContext applicationDbContext, IMediator mediator, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteMetaInformation_Entity command, CancellationToken cancellationToken)
        {
            MetaInformation_Entity_Mapping? metaInformation_Entity = null;

            if (command.MetaInformationId > 0)
            {
                metaInformation_Entity = await _applicationDbContext.MetaInformation_Entity_Mappings
                .Where(e => e.MetaInformationId == command.MetaInformationId)
                .SingleOrDefaultAsync();
            }
            else if (command.EntityId > 0)
            {
                metaInformation_Entity = await _applicationDbContext.MetaInformation_Entity_Mappings
                .Where(e => e.EntityId == command.EntityId && 
                            e.EntityType == command.EntityType)
                .SingleOrDefaultAsync();
            }

            if (metaInformation_Entity == null)
            {
                return true;
            }

            _applicationDbContext.Remove(metaInformation_Entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.METAINFORMATION_ENTITY,
                    TransactionType = TransactionType.DELETE,
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
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            var deleteMetaInformationRequest = new DeleteMetaInformationRequest()
            {
                Id = metaInformation_Entity.MetaInformationId
            };

            var deleteMetaInformationCommand = _mapper.Map(deleteMetaInformationRequest, new DeleteMetaInformation(command.Identity));

            await _mediator.Send(deleteMetaInformationCommand);

            return true;
        }

        #endregion
    }
}
