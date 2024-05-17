using Adanom.Ecommerce.API.Commands.Models;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingProviders : IRequest<PaginatedData<ShippingProviderResponse>>
    {
        #region Ctor

        public GetShippingProviders(PaginationRequest? pagination = null)
        {
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
