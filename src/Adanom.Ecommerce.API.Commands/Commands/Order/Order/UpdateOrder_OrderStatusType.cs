using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class UpdateOrder_OrderStatusType : IRequest<bool>
    {
        #region Ctor

        public UpdateOrder_OrderStatusType(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public OrderStatusType NewOrderStatusType { get; set; }

        public OrderStatusType? OldOrderStatusType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public string? ShippingTrackingCode { get; set; }

        #endregion
    }
}
