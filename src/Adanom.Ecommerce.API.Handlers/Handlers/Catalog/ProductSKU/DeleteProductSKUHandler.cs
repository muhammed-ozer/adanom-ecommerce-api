using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSKUHandler : IRequestHandler<DeleteProductSKU, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductSKUHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSKU command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            var productSKU = await _applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSKU.DeletedAtUtc = DateTime.UtcNow;
            productSKU.DeletedByUserId = userId;

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
