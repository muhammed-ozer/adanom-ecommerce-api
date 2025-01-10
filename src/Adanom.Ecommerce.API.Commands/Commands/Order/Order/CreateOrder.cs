using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateOrder : IRequest<OrderResponse?>
    {
        #region Ctor

        public CreateOrder(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public DeliveryType DeliveryType { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }

        public long ShippingAddressId { get; set; }

        public long? BillingAddressId { get; set; }

        public string? Note { get; set; }

        public bool DistanceSellingContract { get; set; }

        public bool PreliminaryInformationForm { get; set; }

        #endregion
    }
}
