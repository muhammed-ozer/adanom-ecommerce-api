namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequestsCount : IRequest<int>
    {
        #region Ctor

        public GetReturnRequestsCount(GetReturnRequestsCountFilter? filter = null)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetReturnRequestsCountFilter? Filter { get; set; }

        #endregion
    }
}
