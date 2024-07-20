using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateReturnRequest : IRequest<ReturnRequestResponse?>
    {
        #region Ctor

        public CreateReturnRequest(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long OrderId { get; set; }

        public ICollection<CreateReturnRequest_ItemRequest> CreateReturnRequest_ItemRequests { get; set; } = new List<CreateReturnRequest_ItemRequest>();

        #endregion
    }
}
