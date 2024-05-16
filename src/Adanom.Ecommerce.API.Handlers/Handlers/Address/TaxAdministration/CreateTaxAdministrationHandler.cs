using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateTaxAdministrationHandler : IRequestHandler<CreateTaxAdministration, TaxAdministrationResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateTaxAdministrationHandler(
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

        public async Task<TaxAdministrationResponse?> Handle(CreateTaxAdministration command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var taxAdministration = _mapper.Map<CreateTaxAdministration, TaxAdministration>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreateByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(taxAdministration);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.TAXADMINISTRATION,
                    TransactionType = TransactionType.CREATE,
                    Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, taxAdministration.Id),
                }));
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.TAXADMINISTRATION,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                taxAdministration = null;
            }

            var taxAdministrationResponse = _mapper.Map<TaxAdministrationResponse>(taxAdministration);

            return taxAdministrationResponse;
        }

        #endregion
    }
}
