using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductPriceHandler : IRequestHandler<DeleteProductPrice, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductPriceHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductPrice command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productPrice = await _applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            // Check productSKU has deleted before price delete
            var productSKUsThatUseProductPrice = await _applicationDbContext.ProductSKUs
                        .Where(e => e.DeletedAtUtc == null && e.ProductPriceId == productPrice.Id)
                        .ToListAsync();

            if (productSKUsThatUseProductPrice.Any())
            {
                return false;
            }

            productPrice.DeletedAtUtc = DateTime.UtcNow;
            productPrice.DeletedByUserId = userId;

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
