namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSpecificationAttributeGroups : IRequest<PaginatedData<ProductSpecificationAttributeGroupResponse>>
    {
        #region Ctor

        public GetProductSpecificationAttributeGroups(GetProductSpecificationAttributeGroupsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductSpecificationAttributeGroupsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}