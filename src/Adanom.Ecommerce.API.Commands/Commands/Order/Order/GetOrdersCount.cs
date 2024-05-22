namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrdersCount : IRequest<int>
    {
        #region Ctor

        public GetOrdersCount(GetOrdersCountFilter? filter = null)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetOrdersCountFilter? Filter { get; set; }

        #endregion
    }
}
