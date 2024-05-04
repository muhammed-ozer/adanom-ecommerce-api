using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteTaxCategoryHandler : IRequestHandler<DeleteTaxCategory, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteTaxCategoryHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteTaxCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var taxCategory = await _applicationDbContext.TaxCategories
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            taxCategory.DeletedAtUtc = DateTime.UtcNow;
            taxCategory.DeletedByUserId = userId;

            await _applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new RemoveFromCache<TaxCategoryResponse>(taxCategory.Id));

            return true;
        }

        #endregion
    }
}
