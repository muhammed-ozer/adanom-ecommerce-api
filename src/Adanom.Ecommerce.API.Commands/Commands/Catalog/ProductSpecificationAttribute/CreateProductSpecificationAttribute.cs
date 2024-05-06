using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProductSpecificationAttribute : IRequest<ProductSpecificationAttributeResponse?>
    {
        #region Ctor

        public CreateProductSpecificationAttribute(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductSpecificationAttributeGroupId { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int DisplayOrder { get; set; }

        #endregion
    }
}