namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetOrdersCountFilter
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public OrderStatusType? OrderStatusType { get; set; }
    }
}