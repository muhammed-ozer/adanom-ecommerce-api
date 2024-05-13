using System.Security.Claims;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductAttributeHandler : IRequestHandler<DeleteProductAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductAttributeHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productAttribute = await _applicationDbContext.ProductAttributes
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            productAttribute.DeletedAtUtc = DateTime.UtcNow;
            productAttribute.DeletedByUserId = userId;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductAttribute_Delete_Failed: {exception.Message}");

                return false;
            }

            return true;
        }

        #endregion
    }
}
