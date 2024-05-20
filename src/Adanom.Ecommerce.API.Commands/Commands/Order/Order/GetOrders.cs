namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrders : IRequest<PaginatedData<OrderResponse>>
    {
        #region Ctor

        public GetOrders(GetOrdersFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetOrdersFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
