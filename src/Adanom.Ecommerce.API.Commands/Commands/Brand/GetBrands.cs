namespace Adanom.Ecommerce.API.Commands
{
    public class GetBrands : IRequest<PaginatedData<BrandResponse>>
    {
        #region Ctor

        public GetBrands(GetBrandsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetBrandsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
