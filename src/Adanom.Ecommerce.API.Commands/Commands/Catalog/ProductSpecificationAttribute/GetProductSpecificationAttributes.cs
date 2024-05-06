namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSpecificationAttributes : IRequest<PaginatedData<ProductSpecificationAttributeResponse>>
    {
        #region Ctor

        public GetProductSpecificationAttributes(GetProductSpecificationAttributesFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductSpecificationAttributesFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}