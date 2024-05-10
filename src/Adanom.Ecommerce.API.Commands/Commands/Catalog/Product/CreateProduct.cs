using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProduct : IRequest<ProductResponse?>
    {
        #region Ctor

        public CreateProduct(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductCategoryId { get; set; }

        public long? BrandId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsNew { get; set; }

        public int DisplayOrder { get; set; }

        public CreateProductSKURequest CreateProductSKURequest { get; set; } = null!;

        #endregion
    }
}