using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetBillingAddresses : IRequest<PaginatedData<BillingAddressResponse>>
    {
        #region Ctor

        public GetBillingAddresses(ClaimsPrincipal identity, PaginationRequest? pagination = null)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
