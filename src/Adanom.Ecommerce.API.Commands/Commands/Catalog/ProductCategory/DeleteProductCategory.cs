using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class DeleteProductCategory : IRequest<bool>
    {
        #region Ctor

        public DeleteProductCategory(ClaimsPrincipal identity)
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