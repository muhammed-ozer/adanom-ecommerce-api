using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteProductSpecificationAttributeGroup : IRequest<bool>
    {
        #region Ctor

        public DeleteProductSpecificationAttributeGroup(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        #endregion
    }
}