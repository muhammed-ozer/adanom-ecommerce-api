namespace Adanom.Ecommerce.API.Commands
{
    public class GetStockNotificationItems : IRequest<PaginatedData<StockNotificationItemResponse>>
    {
        #region Ctor

        public GetStockNotificationItems(GetStockNotificationItemsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetStockNotificationItemsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
