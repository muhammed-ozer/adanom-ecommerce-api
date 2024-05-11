using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProduct_ProductTag : IRequest<bool>
    {
        #region Ctor

        public CreateProduct_ProductTag(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public string ProductTag_Value { get; set; } = null!;

        #endregion
    }
}