namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetOrdersFilter
    {
        public string? Query { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public OrderStatusType? OrderStatusType { get; set; }

        [DefaultValue(GetOrdersOrderByEnum.CREATED_AT_DESC)]
        public GetOrdersOrderByEnum? OrderBy { get; set; }
    }
}