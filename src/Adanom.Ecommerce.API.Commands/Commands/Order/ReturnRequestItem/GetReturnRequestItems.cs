namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequestItems : IRequest<IEnumerable<ReturnRequestItemResponse>>
    {
        #region Ctor

        public GetReturnRequestItems(GetReturnRequestItemsFilter filter)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetReturnRequestItemsFilter Filter { get; set; }

        #endregion
    }
}
