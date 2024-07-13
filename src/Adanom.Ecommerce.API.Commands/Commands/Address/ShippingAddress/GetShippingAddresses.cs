using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingAddresses : IRequest<PaginatedData<ShippingAddressResponse>>
    {
        #region Ctor

        public GetShippingAddresses(ClaimsPrincipal identity, PaginationRequest? pagination = null)
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
