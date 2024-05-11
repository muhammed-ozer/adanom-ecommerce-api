namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProduct_ProductTagExists : IRequest<bool>
    {
        #region Ctor

        public DoesProduct_ProductTagExists(long productId, string productTag_Value)
        {
            ProductId = productId;
            ProductTag_Value = productTag_Value;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        public string ProductTag_Value { get; set; } = null!;

        #endregion
    }
}