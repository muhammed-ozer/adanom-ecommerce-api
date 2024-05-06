namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSpecificationAttributeGroup : IRequest<ProductSpecificationAttributeGroupResponse?>
    {
        #region Ctor

        public GetProductSpecificationAttributeGroup(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}