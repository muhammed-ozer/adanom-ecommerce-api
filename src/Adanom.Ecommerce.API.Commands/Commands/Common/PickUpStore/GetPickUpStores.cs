namespace Adanom.Ecommerce.API.Commands
{
    public class GetPickUpStores : IRequest<PaginatedData<PickUpStoreResponse>>
    {
        #region Ctor

        public GetPickUpStores(PaginationRequest? pagination = null)
        {
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
