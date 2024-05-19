namespace Adanom.Ecommerce.API.Commands
{
    public class GetFavoriteItems : IRequest<PaginatedData<FavoriteItemResponse>>
    {
        #region Ctor

        public GetFavoriteItems(GetFavoriteItemsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetFavoriteItemsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
