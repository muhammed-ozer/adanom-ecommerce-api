namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductCategories : IRequest<PaginatedData<ProductCategoryResponse>>
    {
        #region Ctor

        public GetProductCategories(GetProductCategoriesFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductCategoriesFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}