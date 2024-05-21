namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequest : IRequest<ReturnRequestResponse?>
    {
        #region Ctor

        public GetReturnRequest(long id)
        {
            Id = id;
        }

        public GetReturnRequest(string returnRequest)
        {
            ReturnRequestNumber = returnRequest;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string? ReturnRequestNumber { get; set; }

        #endregion
    }
}
