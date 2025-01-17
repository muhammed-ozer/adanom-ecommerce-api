namespace Adanom.Ecommerce.API.Commands
{
    public class DoesOrderPaymentPaid : IRequest<bool>
    {
        #region Ctor

        public DoesOrderPaymentPaid(long orderId)
        {
            OrderId = orderId;
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        #endregion
    }
}
