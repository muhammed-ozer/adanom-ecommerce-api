using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductDisplayOrder : IRequest<bool>
    {
        #region Ctor

        public UpdateProductDisplayOrder(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public int Displayorder { get; set; }

        #endregion
    }
}