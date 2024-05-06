using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductCategory : IRequest<ProductCategoryResponse?>
    {
        #region Ctor

        public UpdateProductCategory(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public long? ParentId { get; set; }

        public ProductCategoryLevel ProductCategoryLevel { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }

        #endregion
    }
}