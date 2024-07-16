namespace Adanom.Ecommerce.API.Commands
{
    public class GetProducts : IRequest<ProductCatalogResponse>
    {
        #region Ctor

        public GetProducts(PaginationRequest pagination, GetProductsFilter? filter = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public PaginationRequest Pagination { get; }

        public GetProductsFilter? Filter { get; set; }

        #endregion
    }
}
