namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetOrderPaymentsFilter
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [DefaultValue(GetOrderPaymentsOrderByEnum.CREATED_AT_DESC)]
        public GetOrderPaymentsOrderByEnum? OrderBy { get; set; }
    }
}