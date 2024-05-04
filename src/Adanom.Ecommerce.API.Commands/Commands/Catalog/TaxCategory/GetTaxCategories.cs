namespace Adanom.Ecommerce.API.Commands
{
    public class GetTaxCategories : IRequest<PaginatedData<TaxCategoryResponse>>
    {
        #region Ctor

        public GetTaxCategories(GetTaxCategoriesFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetTaxCategoriesFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
