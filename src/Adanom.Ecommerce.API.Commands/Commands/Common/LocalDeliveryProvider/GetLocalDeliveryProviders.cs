using Adanom.Ecommerce.API.Commands.Models;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetLocalDeliveryProviders : IRequest<PaginatedData<LocalDeliveryProviderResponse>>
    {
        #region Ctor

        public GetLocalDeliveryProviders(PaginationRequest? pagination = null)
        {
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
