namespace Adanom.Ecommerce.API.Commands
{
    public class Order_CreateOrderDocuments : IRequest<CreateOrderDocumentsResponse>
    {
        #region Ctor

        public Order_CreateOrderDocuments(UserResponse user, OrderResponse order, OrderPaymentType orderPaymentType, long orderShippingAddressId, long? orderBillingAddressId = null)
        {
            User = user;
            Order = order;
            OrderPaymentType = orderPaymentType;
            OrderShippingAddressId = orderShippingAddressId;
            OrderBillingAddressId = orderBillingAddressId;
        }

        #endregion

        #region Properties

        public UserResponse User { get; set; }

        public OrderResponse Order { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public long OrderShippingAddressId { get; set; }

        public long? OrderBillingAddressId { get; set; }

        #endregion
    }
}