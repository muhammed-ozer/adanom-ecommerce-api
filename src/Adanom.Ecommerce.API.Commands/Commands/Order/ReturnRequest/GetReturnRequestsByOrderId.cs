namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequestsByOrderId : IRequest<IEnumerable<ReturnRequestResponse>>
    {
        #region Ctor

        public GetReturnRequestsByOrderId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}
