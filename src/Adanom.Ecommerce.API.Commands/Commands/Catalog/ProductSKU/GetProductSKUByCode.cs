namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKUByCode : IRequest<ProductSKUResponse?>
    {
        #region Ctor

        public GetProductSKUByCode(string code)
        {
            Code = code;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        #endregion
    }
}