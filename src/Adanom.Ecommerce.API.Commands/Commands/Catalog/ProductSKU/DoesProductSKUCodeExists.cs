namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProductSKUCodeExists : IRequest<bool>
    {
        #region Ctor

        public DoesProductSKUCodeExists(string code, long? excludedProductSKUId = null)
        {
            Code = code;
            ExcludedProductSKUId = excludedProductSKUId;
        }

        #endregion

        #region Properties

        public string Code { get; }

        public long? ExcludedProductSKUId { get; }

        #endregion
    }
}