namespace Adanom.Ecommerce.API.Commands
{
    public class GetProduct_ProductTags : IRequest<IEnumerable<ProductTagResponse>>
    {
        #region Ctor

        public GetProduct_ProductTags(long productId)
        {
            ProductId = productId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        #endregion
    }
}