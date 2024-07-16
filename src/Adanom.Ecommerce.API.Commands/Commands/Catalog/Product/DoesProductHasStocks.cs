namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProductHasStocks : IRequest<bool>
    {
        #region Ctor

        public DoesProductHasStocks(long productId, int? amount = null)
        {
            ProductId = productId;
            Amount = amount;
        }

        #endregion

        #region Properties

        public long ProductId { get; }

        public int? Amount { get; }

        #endregion
    }
}