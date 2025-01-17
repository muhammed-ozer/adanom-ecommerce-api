namespace Adanom.Ecommerce.API.Commands
{
    public class DoesOrderPaymentExistsByOrderId : IRequest<bool>
    {
        #region Ctor

        public DoesOrderPaymentExistsByOrderId(long orderId)
        {
            OrderId = orderId;
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        #endregion
    }
}
