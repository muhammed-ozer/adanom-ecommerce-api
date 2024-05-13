namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductAttribute : IRequest<ProductAttributeResponse?>
    {
        #region Ctor

        public GetProductAttribute(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}