namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductStockQuantity : IRequest<int>
    {
        #region Ctor

        public GetProductStockQuantity(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}