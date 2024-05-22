namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrdersTotalGrandTotal : IRequest<decimal>
    {
        #region Ctor

        public GetOrdersTotalGrandTotal(GetOrdersTotalGrandTotalFilter? filter = null)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetOrdersTotalGrandTotalFilter? Filter { get; set; }

        #endregion
    }
}