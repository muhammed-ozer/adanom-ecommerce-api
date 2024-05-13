using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductSKUStock : IRequest<bool>
    {
        #region Ctor

        public UpdateProductSKUStock(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }

        #endregion
    }
}