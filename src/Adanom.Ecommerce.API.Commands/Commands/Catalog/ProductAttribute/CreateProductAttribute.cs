using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProductAttribute : IRequest<ProductAttributeResponse?>
    {
        #region Ctor

        public CreateProductAttribute(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        #endregion
    }
}