namespace Adanom.Ecommerce.API.Commands
{
    public class GetProduct_ProductSpecificationAttributes : IRequest<IEnumerable<ProductSpecificationAttributeResponse>>
    {
        #region Ctor

        public GetProduct_ProductSpecificationAttributes(long productId)
        {
            ProductId = productId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        #endregion
    }
}