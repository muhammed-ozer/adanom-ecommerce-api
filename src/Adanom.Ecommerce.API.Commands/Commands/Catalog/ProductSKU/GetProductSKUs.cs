namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKUs : IRequest<PaginatedData<ProductSKUResponse>>
    {
        #region Ctor

        public GetProductSKUs(GetProductSKUsFilter filter, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductSKUsFilter Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
