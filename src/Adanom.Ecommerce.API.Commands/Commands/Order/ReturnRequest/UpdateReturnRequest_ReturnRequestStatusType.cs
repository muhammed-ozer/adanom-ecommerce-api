using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class UpdateReturnRequest_ReturnRequestStatusType : IRequest<bool>
    {
        #region Ctor

        public UpdateReturnRequest_ReturnRequestStatusType(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public ReturnRequestStatusType NewReturnRequestStatusType { get; set; }

        public ReturnRequestStatusType? OldReturnRequestStatusType { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public string? DisapprovedReasonMessage { get; set; }

        #endregion
    }
}
