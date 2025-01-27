using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSKUHandler : IRequestHandler<DeleteProductSKU, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSKUHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSKU command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSKU = await applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSKU.DeletedAtUtc = DateTime.UtcNow;
            productSKU.DeletedByUserId = userId;

            await applicationDbContext.SaveChangesAsync();

            command.AddCacheKey(CacheKeyConstants.ProductSKU.CacheKeyById(productSKU.Id));
            command.AddCacheKey(CacheKeyConstants.ProductSKU.CacheKeyByCode(productSKU.Code));

            return true;
        }

        #endregion
    }
}
