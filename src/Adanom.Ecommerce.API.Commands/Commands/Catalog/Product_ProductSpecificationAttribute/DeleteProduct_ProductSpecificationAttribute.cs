using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteProduct_ProductSpecificationAttribute : IRequest<bool>
    {
        #region Ctor

        public DeleteProduct_ProductSpecificationAttribute(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public long ProductSpecificationAttributeId { get; set; }

        #endregion
    }
}