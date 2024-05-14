using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateMetaInformationHandler : IRequestHandler<CreateMetaInformation, MetaInformationResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateMetaInformationHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<MetaInformationResponse?> Handle(CreateMetaInformation command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var metaInfromation = _mapper.Map<CreateMetaInformation, MetaInformation>(command);

            await _applicationDbContext.AddAsync(metaInfromation);

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
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var metaInfromationResponse = _mapper.Map<MetaInformationResponse>(metaInfromation);

            return metaInfromationResponse;
        }

        #endregion
    }
}
