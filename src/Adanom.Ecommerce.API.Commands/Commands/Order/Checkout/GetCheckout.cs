using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetCheckout : IRequest<CheckoutResponse?>
    {
        #region Ctor

        public GetCheckout(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public DeliveryType DeliveryType { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }

        #endregion
    }
}