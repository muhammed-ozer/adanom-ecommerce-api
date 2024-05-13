using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteProduct_ProductCategory : IRequest<bool>
    {
        #region Ctor

        public DeleteProduct_ProductCategory(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public long ProductCategoryId { get; set; }

        #endregion
    }
}