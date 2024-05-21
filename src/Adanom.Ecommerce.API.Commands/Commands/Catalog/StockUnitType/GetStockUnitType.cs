namespace Adanom.Ecommerce.API.Commands
{
    public class GetStockUnitType : IRequest<StockUnitTypeResponse>
    {
        #region Ctor

        public GetStockUnitType(StockUnitType stockUnitType)
        {
            StockUnitType = stockUnitType;
        }

        #endregion

        #region Properties

        public StockUnitType StockUnitType { get; set; }

        #endregion
    }
}
