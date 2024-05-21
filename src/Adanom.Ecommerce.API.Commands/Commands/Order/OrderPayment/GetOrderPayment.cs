namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderPayment : IRequest<OrderPaymentResponse?>
    {
        #region Ctor

        public GetOrderPayment(long id)
        {
            Id = id;
        }

        public GetOrderPayment(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string? OrderNumber { get; set; }

        #endregion
    }
}
