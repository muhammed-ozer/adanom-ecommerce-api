namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductTags : IRequest<PaginatedData<ProductTagResponse>>
    {
        #region Ctor

        public GetProductTags(GetProductTagsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductTagsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}