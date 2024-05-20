namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCarts : IRequest<PaginatedData<ShoppingCartResponse>>
    {
        #region Ctor

        public GetShoppingCarts(GetShoppingCartsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetShoppingCartsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
