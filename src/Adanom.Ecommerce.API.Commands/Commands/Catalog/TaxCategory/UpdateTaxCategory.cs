using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateTaxCategory : IRequest<TaxCategoryResponse?>
    {
        #region Ctor

        public UpdateTaxCategory(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string GroupName { get; set; } = null!;

        public byte Rate { get; set; }

        #endregion
    }
}