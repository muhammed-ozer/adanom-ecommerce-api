namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequestByOrderId : IRequest<ReturnRequestResponse?>
    {
        #region Ctor

        public GetReturnRequestByOrderId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}
