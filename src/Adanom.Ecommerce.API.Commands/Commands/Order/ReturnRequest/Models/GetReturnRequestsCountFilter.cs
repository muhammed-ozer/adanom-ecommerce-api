namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetReturnRequestsCountFilter
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ReturnRequestStatusType? ReturnRequestStatusType { get; set; }
    }
}