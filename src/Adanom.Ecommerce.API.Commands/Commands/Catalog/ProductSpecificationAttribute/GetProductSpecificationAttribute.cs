namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSpecificationAttribute : IRequest<ProductSpecificationAttributeResponse?>
    {
        #region Ctor

        public GetProductSpecificationAttribute(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}