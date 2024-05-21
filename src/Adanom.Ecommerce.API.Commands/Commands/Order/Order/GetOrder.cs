namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrder : IRequest<OrderResponse?>
    {
        #region Ctor

        public GetOrder(long id)
        {
            Id = id;
        }

        public GetOrder(string orderNumber)
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
