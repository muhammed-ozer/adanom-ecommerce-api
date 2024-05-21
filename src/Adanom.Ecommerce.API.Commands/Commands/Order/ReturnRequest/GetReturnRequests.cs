namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequests : IRequest<PaginatedData<ReturnRequestResponse>>
    {
        #region Ctor

        public GetReturnRequests(GetReturnRequestsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetReturnRequestsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
