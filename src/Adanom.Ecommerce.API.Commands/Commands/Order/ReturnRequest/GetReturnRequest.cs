using System.Security.Claims;

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

        public GetReturnRequest(ClaimsPrincipal identity, long id)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Id = id;
        }

        public GetReturnRequest(ClaimsPrincipal identity, string returnRequest)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            ReturnRequestNumber = returnRequest;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string? ReturnRequestNumber { get; set; }

        public ClaimsPrincipal? Identity { get; }

        #endregion
    }
}
