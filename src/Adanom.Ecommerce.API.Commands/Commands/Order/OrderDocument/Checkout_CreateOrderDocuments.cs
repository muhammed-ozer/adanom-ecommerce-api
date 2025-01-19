namespace Adanom.Ecommerce.API.Commands
{
    public class Checkout_CreateOrderDocuments : IRequest<CreateOrderDocumentsResponse>
    {
        #region Ctor

        public Checkout_CreateOrderDocuments(UserResponse user, CheckoutResponse checkoutResponse, OrderPaymentType orderPaymentType, long shippingAddressId, long? billingAddressId = null)
        {
            User = user;
            CheckoutResponse = checkoutResponse;
            OrderPaymentType = orderPaymentType;
            ShippingAddressId = shippingAddressId;
            BillingAddressId = billingAddressId;
        }

        #endregion

        #region Properties

        public UserResponse User { get; set; }

        public CheckoutResponse CheckoutResponse { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public long ShippingAddressId { get; set; }

        public long? BillingAddressId { get; set; }

        #endregion
    }
}