namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKU : IRequest<ProductSKUResponse?>
    {
        #region Ctor

        public GetProductSKU(long id)
        {
            Id = id;
        }


        public GetProductSKU(string code)
        {
            Code = code;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string Code { get; set; } = null!;

        #endregion
    }
}