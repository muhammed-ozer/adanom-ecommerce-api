namespace Adanom.Ecommerce.API.Commands
{
    public class GetAnonymousShoppingCarts : IRequest<PaginatedData<AnonymousShoppingCartResponse>>
    {
        #region Ctor

        public GetAnonymousShoppingCarts(GetAnonymousShoppingCartsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetAnonymousShoppingCartsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
