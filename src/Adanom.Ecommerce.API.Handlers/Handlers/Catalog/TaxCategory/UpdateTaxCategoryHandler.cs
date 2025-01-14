using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateTaxCategoryHandler : IRequestHandler<UpdateTaxCategory, TaxCategoryResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateTaxCategoryHandler(
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

        public async Task<TaxCategoryResponse?> Handle(UpdateTaxCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var taxCategory = await applicationDbContext.TaxCategories
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            taxCategory = _mapper.Map(command, taxCategory);

            taxCategory.UpdatedAtUtc = DateTime.UtcNow;
            taxCategory.UpdatedByUserId = userId;

            applicationDbContext.Update(taxCategory);
            await applicationDbContext.SaveChangesAsync();

            var taxCategoryResponse = _mapper.Map<TaxCategoryResponse>(taxCategory);

            await _mediator.Publish(new UpdateFromCache<TaxCategoryResponse>(taxCategoryResponse));

            return taxCategoryResponse;
        }

        #endregion
    }
}
