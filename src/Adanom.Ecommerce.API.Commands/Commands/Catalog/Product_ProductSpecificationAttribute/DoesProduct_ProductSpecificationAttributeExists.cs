namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProduct_ProductSpecificationAttributeExists : IRequest<bool>
    {
        #region Ctor

        public DoesProduct_ProductSpecificationAttributeExists(long productId, long productSpecificationAttributeId)
        {
            ProductId = productId;
            ProductSpecificationAttributeId = productSpecificationAttributeId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        public long ProductSpecificationAttributeId { get; set; }

        #endregion
    }
}