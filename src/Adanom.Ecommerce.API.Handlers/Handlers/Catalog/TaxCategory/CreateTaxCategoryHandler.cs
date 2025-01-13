using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateTaxCategoryHandler : IRequestHandler<CreateTaxCategory, TaxCategoryResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateTaxCategoryHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<TaxCategoryResponse?> Handle(CreateTaxCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var taxCategory = _mapper.Map<CreateTaxCategory, TaxCategory>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(taxCategory);

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.TAXCATEGORY,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var taxCategoryResponse = _mapper.Map<TaxCategoryResponse>(taxCategory);

            await _mediator.Publish(new AddToCache<TaxCategoryResponse>(taxCategoryResponse));

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.TAXCATEGORY,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, taxCategory.Id),
            }));

            return taxCategoryResponse;
        }

        #endregion
    }
}
